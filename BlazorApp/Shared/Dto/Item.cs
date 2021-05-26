using System.Collections.Generic;

using LocalisedString = System.Collections.Generic.Dictionary<string, string>;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class Item
    {
        public int item_id { get; set; }
        public int item_category_id { get; set; }
        public LocalisedString name { get; set; }
        public LocalisedString description { get; set; }
        public int faction_id { get; set; }
        public string image_path { get; set; }
        public Ammo ammo { get; set; }
        public List<FireMode> fire_mode { get; set; }
    }
}