using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class FireModeToFireGroup
    {
        [JsonPropertyName("fire_group_id")]
        public string FireGroupId { get; set; }

        [JsonPropertyName("fire_mode_id")]
        public string FireModeId { get; set; }

        [JsonPropertyName("fire_mode_index")]
        public string FireModeIndex { get; set; }

        [JsonPropertyName("fire_group_id_join_fire_group")]
        public FireGroup FireGroup { get; set; }
    }
}