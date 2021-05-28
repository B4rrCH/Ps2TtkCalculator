using System.Text.Json.Serialization;

namespace Ps2TtkCalculator.Shared.Dto
{
    public class FireMode
    {
        [JsonPropertyName("fire_mode_id")]
        public string FireModeId { get; set; }

        [JsonPropertyName("pellets_per_shot")]
        public string PelletsPerShot { get; set; }

        [JsonPropertyName("muzzle_velocity")]
        public string MuzzleVelocity { get; set; }

        [JsonPropertyName("fire_mode_id_join_fire_group_to_fire_mode")]
        public FireModeToFireGroup FireModeToFireGroup { get; set; }

        [JsonPropertyName("fire_mode_2")]
        public FireMode2 FireMode2 { get; set; }
    }
}