namespace Ps2TtkCalculator.Shared.Dto
{
    public class FireMode
    {
        public int reload_chamber_time_ms { get; set; }
        public int pellets_per_shot { get; set; }
        public int muzzle_velocity { get; set; }
        public int item_id { get; set; }
        public int damage_min { get; set; }
        public int damage_max { get; set; }
        public int damage_min_range { get; set; }
        public int damage_max_range { get; set; }
        public int fire_rate_ms { get; set; }

    }
}