using System;
using Xunit;

namespace Ps2TtkCalculator.Tests
{
    public class DamageCalculatorTests
    {
        Weapon TestWeapon { get; } = new Weapon
        {
            Name = "Test Weapon",
            DamageModel = new DamageModel
            {
                MaxDamage = 200,
                MinDamage = 100,
                MaxDamageRange_m = 100,
                MinDamageRange_m = 200
            },
            HeadshotMultiplier = 2.0,
            MagazineSize = 100,
            RefireTime_ms = 1000,
        };

        Target DefaultTarget { get; } = new Target
        {
            MaxHP = 1000,
            ResistanceBodyshot = 0,
            ResistanceHeadshot = 0
        };

        [Fact]
        public void DamagePerBodyShotTest_DefaultTarget_DifferentRanges()
        {
            // Arrange

            // Act
            var damageAt0m = DamageCalculator.DamagePerBodyShot(TestWeapon, DefaultTarget, 0);
            var damageAt100m = DamageCalculator.DamagePerBodyShot(TestWeapon, DefaultTarget, 100);
            var damageAt200m = DamageCalculator.DamagePerBodyShot(TestWeapon, DefaultTarget, 200);
            var damageAt150m = DamageCalculator.DamagePerBodyShot(TestWeapon, DefaultTarget, 150);

            // Assert
            Assert.Equal(200, damageAt0m);
            Assert.Equal(200, damageAt100m);
            Assert.Equal(100, damageAt200m);
            Assert.Equal((100 + 200) / 2, damageAt150m);
        }
    }
}
