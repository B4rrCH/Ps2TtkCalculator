using System.IO;
using System.Linq;
using System.Text.Json;
using Xunit;
using Ps2TtkCalculator.Shared.Dto;

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
            var orionItem = JsonSerializer.Deserialize<Item>(orionJson);

            // Act
            var orion = Weapon.FromItem(orionItem);

            // Assert
            Assert.Equal(WeaponCategory.LMG, orion.WeaponCategory);
            Assert.Equal(2.0, orion.HeadshotMultiplier);
            Assert.Equal(50, orion.MagazineSize);
            Assert.Equal(540, orion.MuzzleVelocity_mps);
        }

        [Fact]
        public void FromJsonList_ExampleQueryResult_NoExceptions()
        {
            // Arrange
            var exampleQueryPath = Path.Combine("Data", "ExampleQueryResult.json");
            var exampleQueryJson = File.ReadAllText(exampleQueryPath);
            var itemList = JsonSerializer.Deserialize<ItemList>(exampleQueryJson);

            // Act
            var weapons = itemList.Items.Select(Weapon.FromItem);

            // Assert
            var weaponList = weapons.ToList();
            Assert.NotEmpty(weaponList);
        }
    }
}
