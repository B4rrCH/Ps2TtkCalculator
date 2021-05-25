using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

using CurvePoint = System.Tuple<double, double>;

namespace Ps2TtkCalculator.Shared.Model
{
    public class TtkCurveCalculator
    {
        private Weapon weapon;
        private Target target;
        private int range_m;
        private double[] weights;
        private int damagePerBodyShot;
        private int damagePerHeadShot;

        private CurvePoint[] curve = null;
        public TtkCurveCalculator(Weapon weapon, Shooter shooter, Target target, int range_m)
        {
            this.weapon = weapon;
            this.target = target;
            this.range_m = range_m;

            this.weights = new double[]
            {
                shooter.Acc * (1 - shooter.Hsr), // Bodyshot 
                shooter.Acc * shooter.Hsr,       // Headshot
                1 - shooter.Acc                  // Miss
            };

            this.damagePerBodyShot = DamageCalculator.DamagePerBodyShot(weapon, target, range_m);
            this.damagePerHeadShot = DamageCalculator.DamagePerHeadShot(weapon, target, range_m);
        }

        public async Task<IEnumerable<CurvePoint>> GetCurve()
        {
            return curve ??= (await Task.FromResult(CalculateCurve().ToArray()));
        }

        private IEnumerable<CurvePoint> CalculateCurve()
        {
            yield return Tuple.Create(0.0, 0.0);
            var cdf = CummulativeDistribution();
            foreach ((var earlier, var later) in cdf.Zip(cdf.Skip(1)))
            {
                yield return Tuple.Create(((double)later.Time_ms / 1000)
                                           + range_m / weapon.MuzzleVelocity_mps,
                                          later.Probability - earlier.Probability);
            }
        }
        private IEnumerable<(int Time_ms, double Probability)> CummulativeDistribution()
        {
            int maxShots = Math.Min(weapon.MagazineSize, 100);
            for (int nrShots = 0; nrShots <= maxShots; ++nrShots)
            {
                yield return (nrShots * weapon.RefireTime_ms, CummulativeDistributionAt(nrShots));
            }
        }

        private double CummulativeDistributionAt(int nrShots)
        {
            double survivalProbability = 0.0;
            for (int nrBodyshots = 0;
                 nrBodyshots <= nrShots && DoesTargetSurvive(nrBodyshots, 0);
                 ++nrBodyshots)
            {
                for (int nrHeadshots = 0;
                     nrBodyshots + nrHeadshots <= nrShots && DoesTargetSurvive(nrBodyshots, nrHeadshots);
                     ++nrHeadshots)
                {
                    int nrMisses = nrShots - nrBodyshots - nrHeadshots;
                    survivalProbability += MultinomialDistribution.DensityAt(new int[] { nrBodyshots, nrHeadshots, nrMisses }, weights);
                }
            }
            return 1 - survivalProbability;
        }

        private bool DoesTargetSurvive(int nrBodyshots, int nrHeadshots)
        {
            return (damagePerBodyShot * nrBodyshots
                   + damagePerHeadShot * nrHeadshots)
                   < target.MaxHp;
        }
    }
}