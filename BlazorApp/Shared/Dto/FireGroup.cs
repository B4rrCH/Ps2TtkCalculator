using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class FireGroup
    {
        [JsonPropertyName("chamber_duration_ms")]
        public string ChamberDurationMs { get; set; }
    }
}