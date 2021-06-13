using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ps2TtkCalculator.Shared.Model;
using Ps2TtkCalculator.Shared.Dto;
using Ps2TtkCalculator.Shared.ExtensionMethods;

namespace Ps2TtkCalculator.Shared.Views
{
    public class WeaponPreview
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public string WeaponCategory { get; set; }
        public string ImagePath { get; set; }

        public static WeaponPreview FromItem(Item item) => new ()
        {
            ItemId = int.Parse(item.ItemId),
            Name = item.Name.En,
            Faction = Enum.Parse<Faction>(item.Faction ?? "NaniteSystems").GetDescription(),
            WeaponCategory = Enum.Parse<WeaponCategory>(item.WeaponCategory).GetDescription(),
            ImagePath = item.ImagePath
        };
    }
}
