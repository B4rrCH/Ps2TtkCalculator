namespace Ps2TtkCalculator.Shared.Model
{
    public class Player
    {
        public Weapon Weapon { get; set; } = new();
        public Target Target { get; set; } = new();
        public Shooter Shooter { get; set; } = new();
    }
}