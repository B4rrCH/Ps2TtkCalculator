using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class Item
    {
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }

        [JsonPropertyName("item_category_id")]
        public string WeaponCategory { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("faction_id")]
        public string Faction { get; set; }

        [JsonPropertyName("image_path")]
        public string ImagePath { get; set; }

        [JsonPropertyName("fire_mode")]
        public FireMode FireMode { get; set; }

        [JsonPropertyName("ammo")]
        public Ammo Ammo { get; set; }
    }
}