using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class FireMode2
    {
        [JsonPropertyName("damage_head_multiplier")]
        public string DamageHeadMultiplier { get; set; }

        [JsonPropertyName("fire_refire_ms")]
        public string FireRefireMs { get; set; }

        [JsonPropertyName("max_damage")]
        public string MaxDamage { get; set; }

        [JsonPropertyName("max_damage_range")]
        public string MaxDamageRange { get; set; }

        [JsonPropertyName("min_damage")]
        public string MinDamage { get; set; }

        [JsonPropertyName("min_damage_range")]
        public string MinDamageRange { get; set; }
    }
}