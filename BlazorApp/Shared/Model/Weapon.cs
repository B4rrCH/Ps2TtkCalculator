using System;
using Microsoft.AspNetCore.Components;
using Ps2TtkCalculator.Shared.Dto;

namespace Ps2TtkCalculator.Shared.Model
{
    public class Weapon
    {
        private string name = "Default Weapon";
        private Faction faction = Faction.NaniteSystems;
        private WeaponCategory weaponCategory = WeaponCategory.LMG;
        private string imagePath = null;
        private int magazineSize = 50;
        private int refireTime_ms = 80;
        private double headshotMultiplier = 2.0;
        private int muzzleVelocity_mps = 540;
        private DamageModel damageModel = new();
        private EventCallback propertiesChanged;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public Faction Faction
        {
            get => faction;
            set
            {
                if (faction != value)
                {
                    faction = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public WeaponCategory WeaponCategory
        {
            get => weaponCategory;
            set
            {
                if (weaponCategory != value)
                {
                    weaponCategory = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public string ImagePath
        {
            get => imagePath;
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int MagazineSize
        {
            get => magazineSize;
            set
            {
                if (magazineSize != value)
                {
                    magazineSize = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int RefireTime_ms
        {
            get => refireTime_ms;
            set
            {
                if (refireTime_ms != value)
                {
                    refireTime_ms = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public double HeadshotMultiplier
        {
            get => headshotMultiplier;
            set
            {
                if (headshotMultiplier != value)
                {
                    headshotMultiplier = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public int MuzzleVelocity_mps
        {
            get => muzzleVelocity_mps;
            set
            {
                if (muzzleVelocity_mps != value)
                {
                    muzzleVelocity_mps = value;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }
        public DamageModel DamageModel
        {
            get => damageModel;
            set
            {
                if (damageModel != value)
                {
                    damageModel = value;
                    damageModel.PropertiesChanged = this.PropertiesChanged;
                    PropertiesChanged.InvokeAsync();
                }
            }
        }

        public EventCallback PropertiesChanged 
        { 
            get => propertiesChanged;
            set 
            { 
                propertiesChanged = value;
                damageModel.PropertiesChanged = value;
            }
        }

        public static Weapon FromItem(Item item)
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
            WeaponCategory weaponCategory = (WeaponCategory)int.Parse(item.WeaponCategory);

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