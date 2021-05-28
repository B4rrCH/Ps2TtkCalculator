using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Ps2TtkCalculator.Shared.Model.Tests
{
    public class WeaponTests
    {
        private class Counter
        {
            public int? returned { get; set; }
        }

        [Fact]
        public void FromJson_SingleQueryResult_CanBeParsed()
        {
            // Arrange
            var orionFilePath = Path.Combine("Data", "Orion.json");
            var orionJson = File.ReadAllText(orionFilePath);

            // Act
            var orion = Weapon.FromJson(orionJson);

            // Assert
            Assert.Equal(WeaponCategory.LMG, orion.WeaponCategory);
            Assert.Equal(2.0, orion.HeadshotMultiplier);
            Assert.Equal(50, orion.MagazineSize);
            Assert.Equal(540, orion.MuzzleVelocity_mps);
        }

        [Fact]
        public void FromJsonList_QueryResult_HasRightNumber()
        {
            // Arrange
            var exampleQueryPath = Path.Combine("Data", "ExampleQueryResult.json");
            var exampleQueryJson = File.ReadAllText(exampleQueryPath);
            var count = System.Text.Json.JsonSerializer.Deserialize<Counter>(exampleQueryJson).returned;

            // Act
            var weapons = Weapon.FromJsonList(exampleQueryJson).ToList();

            // Assert
            Assert.Equal(count, weapons.Count());
        }
    }
}
