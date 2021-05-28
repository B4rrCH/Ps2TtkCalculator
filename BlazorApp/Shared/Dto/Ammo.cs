using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class Ammo
    {
        [JsonPropertyName("clip_size")]
        public string ClipSize { get; set; }
    }
}