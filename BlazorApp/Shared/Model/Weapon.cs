namespace Ps2TtkCalculator.Shared.Model
{
    public class Weapon
    {
        public string Name { get; set; } = "Default Weapon";
        public int MagazineSize { get; set; } = 50;
        public int RefireTime_ms { get; set; } = 80;
        public double HeadshotMultiplier { get; set; } = 2.0;
        public int MuzzleVelocity_mps { get; set; } = 540;
        public DamageModel DamageModel { get; set; } = new();
    }
}