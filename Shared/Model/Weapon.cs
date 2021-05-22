namespace Ps2TtkCalculator
{
  public class Weapon
  {
    public string Name { get; set; }
    public int MagazineSize { get; set; }
    public int RefireTime_ms { get; set; }
    public DamageModel DamageModel { get; set; }
  }
}