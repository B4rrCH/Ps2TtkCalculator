using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class Name
    {
        [JsonPropertyName("en")]
        public string En { get; set; }
    }
}