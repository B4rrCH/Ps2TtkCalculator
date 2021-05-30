using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CurvePoint = System.Tuple<double, double>;

namespace Ps2TtkCalculator.Shared.Model
{
    public class TtkCurveCalculator
    {
        private readonly Weapon weapon;
        private readonly Target target;
        private readonly double[] weights;
        private readonly int damagePerBodyShot;
        private readonly int damagePerHeadShot;
        private readonly double bulletTravelTime_s;

        private CurvePoint[] density = null;
        private CurvePoint[] cummulativeDistribution = null;

        public TtkCurveCalculator(Weapon weapon, Shooter shooter, Target target, int range_m)
        {
            this.weapon = weapon;
            this.target = target;

            this.weights = new double[]
            {
                shooter.Acc * (1 - shooter.Hsr), // Bodyshot 
                shooter.Acc * shooter.Hsr,       // Headshot
                1 - shooter.Acc                  // Miss
            };

            this.damagePerBodyShot = DamageCalculator.DamagePerBodyShot(weapon, target, range_m);
            this.damagePerHeadShot = DamageCalculator.DamagePerHeadShot(weapon, target, range_m);

            this.bulletTravelTime_s = (double)range_m / weapon.MuzzleVelocity_mps;
        }

        public async Task<IEnumerable<CurvePoint>> GetCurve()
        {
            return density ??= (await Task.FromResult(CalculateCurve().ToArray()));
        }

        public async Task<IEnumerable<CurvePoint>> GetCumulativeDistribution()
        {
            return cummulativeDistribution ??= (await Task.FromResult(CalculateCummulativeDistribution().ToArray()));
        }

        private IEnumerable<CurvePoint> CalculateCurve()
        {
            cummulativeDistribution = CalculateCummulativeDistribution().ToArray();
            foreach ((var earlier, var later) in cummulativeDistribution.Zip(cummulativeDistribution.Skip(1)))
            {
                yield return Tuple.Create(earlier.Item1, later.Item2 - earlier.Item2);
            }
        }
        private IEnumerable<CurvePoint> CalculateCummulativeDistribution()
        {
            int maxShots = Math.Min(weapon.MagazineSize, 100);
            for (int nrShots = 0; nrShots <= maxShots; ++nrShots)
            {
                yield return Tuple.Create(nrShots * weapon.RefireTime_ms / 1000.0 + bulletTravelTime_s, 
                                          CummulativeDistributionAt(nrShots));
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