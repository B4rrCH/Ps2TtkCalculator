using System;
using Xunit;

namespace Ps2TtkCalculator.Shared.Model.Tests
{
    public class DamageCalculatorTests
    {
        private const int maxDamage = 200;
        private const int minDamage = 100;
        private const int maxDamageRange = 100;
        private const int minDamageRange = 200;

        Weapon TestWeapon => new Weapon
        {
            Name = "Test Weapon",
            DamageModel = new DamageModel
            {
                MaxDamage = maxDamage,
                MinDamage = minDamage,
                MaxDamageRange_m = maxDamageRange,
                MinDamageRange_m = minDamageRange
            },
            HeadshotMultiplier = 2.0,
            MagazineSize = 100,
            RefireTime_ms = 1000,
        };

        [Fact]
        public void DamagePerBodyShotTest_DefaultTarget_DifferentRanges()
        {
            // Arrange
            Target defaultTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.0,
                ResistanceHeadshot = 0.0
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerBodyShot(TestWeapon, defaultTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, defaultTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, defaultTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerBodyShot(TestWeapon, defaultTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(maxDamage, damageAt0m);
            Assert.Equal(maxDamage, damageAtMaxDamageRange);
            Assert.Equal(minDamage, damageAtMinDamageRange);
            Assert.Equal((minDamage + maxDamage) / 2, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerHeadShotTest_DefaultTarget_DifferentRanges()
        {
            // Arrange
            Target defaultTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.0,
                ResistanceHeadshot = 0.0
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerHeadShot(TestWeapon, defaultTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, defaultTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, defaultTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerHeadShot(TestWeapon, defaultTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(2 * maxDamage, damageAt0m);
            Assert.Equal(2 * maxDamage, damageAtMaxDamageRange);
            Assert.Equal(2 * minDamage, damageAtMinDamageRange);
            Assert.Equal(2 * (minDamage + maxDamage) / 2, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerBodyShotTest_NanoWeaveTarget_DifferentRanges()
        {
            // Arrange
            Target nanoWeaveTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.2,
                ResistanceHeadshot = 0.0
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerBodyShot(TestWeapon, nanoWeaveTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, nanoWeaveTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, nanoWeaveTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerBodyShot(TestWeapon, nanoWeaveTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(maxDamage * 8 / 10, damageAt0m);
            Assert.Equal(maxDamage * 8 / 10, damageAtMaxDamageRange);
            Assert.Equal(minDamage * 8 / 10, damageAtMinDamageRange);
            Assert.Equal((minDamage + maxDamage) / 2 * 8 / 10, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerHeadShotTest_NanoWeaveTarget_DifferentRanges()
        {
            // Arrange
            Target nanoWeaveTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.2,
                ResistanceHeadshot = 0.0
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerHeadShot(TestWeapon, nanoWeaveTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, nanoWeaveTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, nanoWeaveTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerHeadShot(TestWeapon, nanoWeaveTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(2 * maxDamage, damageAt0m);
            Assert.Equal(2 * maxDamage, damageAtMaxDamageRange);
            Assert.Equal(2 * minDamage, damageAtMinDamageRange);
            Assert.Equal(2 * (minDamage + maxDamage) / 2, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerBodyShotTest_SymbiotTarget_DifferentRanges()
        {
            // Arrange
            Target symbioteTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.2,
                ResistanceHeadshot = 0.2
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerBodyShot(TestWeapon, symbioteTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, symbioteTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, symbioteTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerBodyShot(TestWeapon, symbioteTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(maxDamage * 8 / 10, damageAt0m);
            Assert.Equal(maxDamage * 8 / 10, damageAtMaxDamageRange);
            Assert.Equal(minDamage * 8 / 10, damageAtMinDamageRange);
            Assert.Equal((minDamage + maxDamage) / 2 * 8 / 10, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerHeadShotTest_SymbiotTarget_DifferentRanges()
        {
            // Arrange
            Target symbioteTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.2,
                ResistanceHeadshot = 0.2
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerHeadShot(TestWeapon, symbioteTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, symbioteTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, symbioteTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerHeadShot(TestWeapon, symbioteTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(2 * maxDamage * 8 / 10, damageAt0m);
            Assert.Equal(2 * maxDamage * 8 / 10, damageAtMaxDamageRange);
            Assert.Equal(2 * minDamage * 8 / 10, damageAtMinDamageRange);
            Assert.Equal(2 * (minDamage + maxDamage) / 2 * 8 / 10, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerBodyShotTest_ResistShieldTarget_DifferentRanges()
        {
            // Arrange
            Target resistShieldTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.35,
                ResistanceHeadshot = 0.35
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerBodyShot(TestWeapon, resistShieldTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, resistShieldTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerBodyShot(TestWeapon, resistShieldTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerBodyShot(TestWeapon, resistShieldTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(maxDamage * 65 / 100, damageAt0m);
            Assert.Equal(maxDamage * 65 / 100, damageAtMaxDamageRange);
            Assert.Equal(minDamage * 65 / 100, damageAtMinDamageRange);
            Assert.Equal((minDamage + maxDamage) / 2 * 65 / 100, damageAtBetweenMaxAndMinRange);
        }

        [Fact]
        public void DamagePerHeadShotTest_ResistShieldTarget_DifferentRanges()
        {
            // Arrange
            Target resistShieldTarget = new Target
            {
                MaxHealthPoints = 1000,
                ResistanceBodyshot = 0.35,
                ResistanceHeadshot = 0.35
            };

            // Act
            var damageAt0m = DamageCalculator.DamagePerHeadShot(TestWeapon, resistShieldTarget, 0);
            var damageAtMaxDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, resistShieldTarget, maxDamageRange);
            var damageAtMinDamageRange = DamageCalculator.DamagePerHeadShot(TestWeapon, resistShieldTarget, minDamageRange);
            var damageAtBetweenMaxAndMinRange = DamageCalculator.DamagePerHeadShot(TestWeapon, resistShieldTarget, (maxDamageRange + minDamageRange) / 2);

            // Assert
            Assert.Equal(2 * maxDamage * 65 / 100, damageAt0m);
            Assert.Equal(2 * maxDamage * 65 / 100, damageAtMaxDamageRange);
            Assert.Equal(2 * minDamage * 65 / 100, damageAtMinDamageRange);
            Assert.Equal(2 * (minDamage + maxDamage) / 2 * 65 / 100, damageAtBetweenMaxAndMinRange);
        }
    }
}
