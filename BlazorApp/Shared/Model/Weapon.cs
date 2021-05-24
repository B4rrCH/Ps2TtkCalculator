namespace Ps2TtkCalculator.Shared.Model
{
    public class Weapon
    {
        public string Name { get; set; }
        public int MagazineSize { get; set; }
        public int RefireTime_ms { get; set; }
        public double HeadshotMultiplier { get; set; }
        public int MuzzleVelocity_mps { get; set; }
        public DamageModel DamageModel { get; set; }
    }
}