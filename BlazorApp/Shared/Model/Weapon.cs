using System;
using Ps2TtkCalculator.Shared.Dto;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Weapon
    {
        public string Name { get; set; } = "Default Weapon";
        public Faction Faction { get; set; } = Faction.NaniteSystems;
        public WeaponCategory WeaponCategory { get; set; } = WeaponCategory.LMG;
        public string ImagePath { get; set; } = null;
        public int MagazineSize { get; set; } = 50;
        public int RefireTime_ms { get; set; } = 80;
        public double HeadshotMultiplier { get; set; } = 2.0;
        public int MuzzleVelocity_mps { get; set; } = 540;
        public DamageModel DamageModel { get; set; } = new();

        public static Weapon FromItem(Dto.Item item)
        {
            string name = item.Name?.En;
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (name == "Mystery Weapon" || name == "NS-03 Thumper") return null;
            int pelletCount = int.Parse(item.FireMode.PelletsPerShot);
            var damageModel = new DamageModel
            {
                MaxDamage = int.Parse(item.FireMode.FireMode2.MaxDamage) * pelletCount,
                MinDamage = int.Parse(item.FireMode.FireMode2.MinDamage) * pelletCount,
                MaxDamageRange_m = int.Parse(item.FireMode.FireMode2.MaxDamageRange),
                MinDamageRange_m = int.Parse(item.FireMode.FireMode2.MinDamageRange)
            };


            Faction faction = (Faction)int.Parse(item.Faction ?? "0");
            WeaponCategory weaponCategory = (Model.WeaponCategory)int.Parse(item.WeaponCategory);

            if (!int.TryParse(item.Ammo?.ClipSize, out int clipSize))
            {
                clipSize = GetClipSizeHardcodedCases(item);
            }

            int refireTimeNonBoltAction_ms = int.Parse(item.FireMode.FireMode2.FireRefireMs);
            int boltActionCyclingTime_ms = int.Parse(item.FireMode.FireModeToFireGroup.FireGroup?.ChamberDurationMs ?? "0");

            if (!double.TryParse(item.FireMode.FireMode2.DamageHeadMultiplier,
                                 out double damageHeadMultiplier))
            {
                if (name.Contains("Soldier Soaker"))
                {
                    damageHeadMultiplier = 0.0;
                }
            }

            if (!int.TryParse(item.FireMode.MuzzleVelocity
                              ?? item.FireMode.Speed,
                              out int muzzleVelocity_mps))
            {
                throw new ArgumentException($"Could not determine muzzle velocity of {name}.");
            }

            var weapon = new Weapon
            {
                Name = name,
                Faction = faction,
                WeaponCategory = weaponCategory,
                ImagePath = item.ImagePath,
                MagazineSize = clipSize,
                RefireTime_ms = refireTimeNonBoltAction_ms + boltActionCyclingTime_ms,
                HeadshotMultiplier = 1.0 + damageHeadMultiplier,
                MuzzleVelocity_mps = muzzleVelocity_mps,
                DamageModel = damageModel
            };

            return weapon;
        }

        private static int GetClipSizeHardcodedCases(Item item)
        {
            string name = item.Name.En;
            return name.Contains("Phaseshift") ? 100 :
                   name.Contains("Soldier Soaker") ? 100 :
                   name.Contains("Flare Gun") ? 2 :
                   name.Contains("Deep Freeze") ? 2 :
                   name.Contains("CandyCannon") ? 3 :
                   name.Contains("Blackhand") ? 4 :
                   name.Contains("Showdown") ? 4 :
                   throw new ArgumentException($"Could not determine clip size of {name}.");
        }
    }
}