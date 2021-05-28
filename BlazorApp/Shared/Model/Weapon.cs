using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
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

            int boltActionCyclingTime_ms = int.Parse(item.FireMode.FireModeToFireGroup.FireGroup?.ChamberDurationMs ?? "0");

            Faction faction = (Faction)int.Parse(item.Faction ?? "0");
            WeaponCategory weaponCategory = (Model.WeaponCategory)int.Parse(item.WeaponCategory);

            if (!int.TryParse(item.Ammo?.ClipSize, out int clipSize))
            {
                if (name.Contains("Phaseshift")) clipSize = 100;
            }
            int v1 = int.Parse(item.FireMode.FireMode2.FireRefireMs);
            if (!double.TryParse(item.FireMode.FireMode2.DamageHeadMultiplier, out double damageHeadMultiplier))
            {
                if (name.Contains("Soldier Soaker"))
                {
                    damageHeadMultiplier = 0.0;
                }
            }

            if (!int.TryParse(item.FireMode.MuzzleVelocity ?? "", out int muzzleVelocity_mps))
            {
                muzzleVelocity_mps = GetMuzzleVelocityHardcodedCases(item);
            }

            var weapon = new Weapon
            {
                Name = name,
                Faction = faction,
                WeaponCategory = weaponCategory,
                ImagePath = item.ImagePath,
                MagazineSize = clipSize,
                RefireTime_ms = v1
                                + boltActionCyclingTime_ms,
                HeadshotMultiplier = 1.0 + damageHeadMultiplier,
                MuzzleVelocity_mps = muzzleVelocity_mps,
                DamageModel = damageModel
            };

            return weapon;
        }

        private static int GetMuzzleVelocityHardcodedCases(Item item)
        {
            if (int.TryParse(item.FireMode.MuzzleVelocity, out int muzzleVelocity))
            {
                return muzzleVelocity;
            }

            else if ((Model.WeaponCategory)int.Parse(item.WeaponCategory) == WeaponCategory.Crossbow)
            {
                return 150;
            }

            string name = item.Name.En;
            string[] weaponsWithMuzzleVelocity550 = { "Showdown", "Maw", "Watchman", "Bishop", "Obelisk", "Dragoon", "Promise", "Tranquility" };

            if (weaponsWithMuzzleVelocity550.Any(x => name.Contains(x)))
            {
                return 550;
            }

            return name.Contains("Flare Gun") ? 50 :
                   name.Contains("Deep Freeze") ? 50 :
                   name.Contains("Naginata") ? 490 :
                   name.Contains("Tomoe") ? 750 :
                   name.Contains("Daimyo") ? 600 :
                   name.Contains("Pilot") ? 350 :
                   name.Contains("Canis") ? 375 :
                   name.Contains("Soldier Soaker") ? 100 :
                   name.Contains("Ectoblaster") ? 150 :
                   name.Contains("Kindred") ? 480 :
                   name.Contains("Charger") ? 500 :
                   name.Contains("Horizon") ? 500 :
                   name.Contains("Yawara") ? 500 :
                   name.Contains("Sesshin") ? 520 :
                   name.Contains("Vanquisher") ? 600 :
                   name.Contains("Arbalest") ? 600 :
                   name.Contains("Lacerta") ? 600 :
                   name.Contains("SR-200") ? 600 :
                   name.Contains("Punisher") ? 350 :
                   throw new System.Exception($"Could not determine Muzzle Velocity of {name}.");
        }
    }
}