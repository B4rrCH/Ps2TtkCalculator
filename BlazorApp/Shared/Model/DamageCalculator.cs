namespace Ps2TtkCalculator
{
    public static class DamageCalculator
    {
        public static int DamagePerBodyShot(Weapon weapon, Target target, int range_m)
        {
            double RawDamage = weapon.GetRawDamageAt(range_m);
            return (int)(RawDamage * (1 - target.ResistanceBodyshot));
        }

        public static int DamagePerHeadShot(Weapon weapon, Target target, int range_m)
        {
            double RawDamage = weapon.GetRawDamageAt(range_m);
            return (int)(RawDamage * weapon.HeadshotMultiplier * (1 - target.ResistanceHeadshot));
        }

        private static double GetRawDamageAt(this Weapon weapon, int range_m)
        {
            if (weapon.DamageModel.MaxDamageRange_m < range_m)
            {
                return weapon.DamageModel.MaxDamage;
            }
            else if (range_m < weapon.DamageModel.MinDamageRange_m)
            {
                return weapon.DamageModel.MinDamage;
            }
            else
            {
                // range = lambda * MaxRange + (1- lambda) * MinRange =>
                var lambda = (double)(range_m - weapon.DamageModel.MinDamageRange_m)
                             / (weapon.DamageModel.MaxDamageRange_m - weapon.DamageModel.MinDamageRange_m);
                return lambda * weapon.DamageModel.MaxDamage
                       + (1 - lambda) * weapon.DamageModel.MinDamage;
            }

        }
    }
}