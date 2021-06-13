using System.ComponentModel;

namespace Ps2TtkCalculator.Shared.Model
{
    public enum WeaponCategory
    {
        Pistol = 3,
        Shotgun = 4,
        SMG = 5,
        LMG = 6,
        [Description("Assault Rifle")]
        AssaultRifle = 7,
        Carbine = 8,
        [Description("Sniper Rifle")]
        SniperRifle = 11,
        [Description("Scout Rifle")]
        ScoutRifle = 12,
        [Description("Heavy Weapon")]
        HeavyWeapon = 14,
        [Description("Battle Rifle")]
        BattleRifle = 19,
        Crossbow = 24
    }
}