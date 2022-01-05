namespace Ps2TtkCalculator.Shared.Model
{
    public static class DamageCalculator
    {
        public static int DamagePerBodyShot(Weapon weapon, Target target, int range_m)
        {
            var rawDamage = GetRawDamageAt(weapon, range_m);
            return (int)(rawDamage * (1 - target.ResistanceBodyshot));
        }

        public static int DamagePerHeadShot(Weapon weapon, Target target, int range_m)
        {
            var rawDamage = GetRawDamageAt(weapon, range_m);
            return (int)(rawDamage * weapon.HeadshotMultiplier * (1 - target.ResistanceHeadshot));
        }

        private static double GetRawDamageAt(Weapon weapon, int range_m)
        {
            if (range_m < weapon.DamageModel.MaxDamageRange_m)
            {
                return weapon.DamageModel.MaxDamage;
            }
            else if (range_m < weapon.DamageModel.MinDamageRange_m)
            {
                // range = lambda * MaxRange + (1 - lambda) * MinRange
                var lambda = (double)(range_m - weapon.DamageModel.MinDamageRange_m)
                             / (weapon.DamageModel.MaxDamageRange_m - weapon.DamageModel.MinDamageRange_m);
                return lambda * weapon.DamageModel.MaxDamage
                       + (1 - lambda) * weapon.DamageModel.MinDamage;
            }
            else
            {
                return weapon.DamageModel.MinDamage;
            }
        }
    }
}