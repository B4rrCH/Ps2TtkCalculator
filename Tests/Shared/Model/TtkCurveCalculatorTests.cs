using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Ps2TtkCalculator.Shared.Dto;

namespace Ps2TtkCalculator.Shared.Model.Tests
{
    public class TtkCurveCalculatorTests
    {
        [Fact]
        public async Task GetCurve_PerfectShooter_SumOfValuesIsOne()
        {
            // Arrange
            Weapon orion = new(); // Default is Orion

            var shooter = new Shooter()
            {
                Acc = 1.0,
                Hsr = 1.0
            };
            var target = new Target();

            var range_m = 0;

            // Act
            var calc = new TtkCurveCalculator(orion, shooter, target, range_m);
            var x = await calc.GetCurve();
            var sum = x.Sum(x => x.Item2);

            // Assert
            Assert.Equal(1.0, sum, 1);
        }

        [Fact]
        public async Task GetCurve_AwefulShooter_SumOfValuesIsZero()
        {
            // Arrange
            Weapon orion = new(); // Default is Orion

            var shooter = new Shooter()
            {
                Acc = 0.0,
                Hsr = 0.0
            };
            var target = new Target();

            var range_m = 0;

            // Act
            var calc = new TtkCurveCalculator(orion, shooter, target, range_m);
            var x = await calc.GetCurve();
            var sum = x.Sum(x => x.Item2);

            // Assert
            Assert.Equal(0.0, sum, 1);
        }


        [Fact]
        public async Task GetCurve_DecentShooter_SumOfValuesIsAboutOne()
        {
            // Arrange
            Weapon orion = new(); // Default is Orion

            var shooter = new Shooter()
            {
                Acc = 0.5,
                Hsr = 0.5
            };
            var target = new Target();

            var range_m = 0;

            // Act
            var calc = new TtkCurveCalculator(orion, shooter, target, range_m);
            var x = await calc.GetCurve();
            var sum = x.Sum(x => x.Item2);

            // Assert
            Assert.InRange(sum, 0.95, 1.0);
        }
    }
}
