using System;
using System.Collections.Generic;
using System.Linq;

using CurvePoint = System.Tuple<double, double>;

namespace Ps2TtkCalculator.Shared.Model
{
    public static class WinProbabilityCalculator
    {
        public static WinProbabilities GetProbabilites(IEnumerable<CurvePoint> density1,
                                                       IEnumerable<CurvePoint> cummulativeDistribution1,
                                                       IEnumerable<CurvePoint> density2,
                                                       IEnumerable<CurvePoint> cummulativeDistribution2)
            => new()
            {
                Player1Wins = GetProbabilityPlayer1Wins(density1, cummulativeDistribution2),
                Player2Wins = GetProbabilityPlayer1Wins(density2, cummulativeDistribution1),
                Draw = GetProbabilityOfDraw(cummulativeDistribution1, cummulativeDistribution2),
                Trade = GetProbabilityOfKillTrade(density1, density2)
            };


        private static double GetProbabilityOfDraw(IEnumerable<CurvePoint> cummulativeDistribution1, 
                                                   IEnumerable<CurvePoint> cummulativeDistribution2) =>
            (1.0 - cummulativeDistribution1.Last().Item2) * (1.0 - cummulativeDistribution2.Last().Item2);

        private static double GetProbabilityPlayer1Wins(IEnumerable<CurvePoint> density1,
                                                        IEnumerable<CurvePoint> cummulativeDistribution2)
        {
            // P(X < Y) = ∑ P(Y < t) * P(X = t)
            double RiemanStiltjesFormula(CurvePoint time_density)
            {
                CurvePoint lastBefore = cummulativeDistribution2.LastOrDefault(t => t.Item1 <= time_density.Item1);
                return time_density.Item2 * (lastBefore?.Item2 ?? 0.0);
            }
            
            return density1.Sum(RiemanStiltjesFormula);
        }

        private static double GetProbabilityOfKillTrade(IEnumerable<CurvePoint> density1,
                                                        IEnumerable<CurvePoint> density2)
        {
            // P(X = Y) = ∑ P(Y = t) * P(X = t)
            double RiemanStiltjesFormula(CurvePoint time_density)
            {
                double p1 = time_density.Item2;
                double p2 = density2.FirstOrDefault(t => t.Item1 == time_density.Item1)?.Item2 ?? 0.0;
                return p1 * p2;
            }
            return density1.Sum(RiemanStiltjesFormula);
        }
    }
}
