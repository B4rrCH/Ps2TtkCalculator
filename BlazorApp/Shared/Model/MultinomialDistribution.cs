using System.Collections.Generic;
using System.Linq;

namespace Ps2TtkCalculator.Shared.Model
{
    public static class MultinomialDistribution
    {
        public static double DensityAt(
            IEnumerable<int> ks,
            IEnumerable<double> weights)
        {
            var weightSum = weights.Sum();
            var probabilities = weights.Select(x => x / weightSum);

            var res = 1.0;
            var n = 1;
            foreach ((var k, var p) in ks.Zip(probabilities))
            {
                for (var i = 1; i <= k; ++i)
                {
                    res *= n++; // => (k1 + k2 + ... + km)!
                    res /= i;   // => 1 / (k1! k2! k3! ... km!)
                    res *= p;   // => p1^k1 p2^k2 ... pm^km
                }
            }
            return res;
        }
    }
}