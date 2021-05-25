using System;
using System.Linq;
using Xunit;

namespace Ps2TtkCalculator.Shared.Model.Tests
{
    public class MultinomialDistributionTests
    {
        [Fact]
        public void MultinomialDistribution_OfSingleNumber_IsOne()
        {
            foreach (int i in new[] { 1, 2, 8, 42 })
            {
                var oneOfI = MultinomialDistribution.DensityAt(new int[] { i }, new double[] { 1.0 });

                Assert.Equal(1, oneOfI);
            }
        }

        [Fact]
        public void BinomialDistribution_WithCertainSingleOutcome_HasCertainCombinedOutcome()
        {
            foreach (int i in new[] { 1, 2, 8, 42 })
            {
                var allCertain = MultinomialDistribution.DensityAt(new int[] { i, 0 }, new double[] { 1.0, 0.0 });

                Assert.Equal(1, allCertain);
            }

            foreach (int i in new[] { 1, 2, 8, 42 })
            {
                var allCertain = MultinomialDistribution.DensityAt(new int[] { i, 0, 0 }, new double[] { 1.0, 0.0, 0.0 });

                Assert.Equal(1, allCertain);
            }
        }

        [Fact]
        public void MultinomialDistribution_WithNEqual1AndSameWeights_IsUniform()
        {
            foreach (int i in new[] { 1, 2, 8, 42 })
            {
                var density = Enumerable.Repeat(1.0, i);

                for (int j = 0; j < i; ++j)
                {
                    var dirac = Enumerable.Repeat(0, j).Concat(new[] { 1 }).Concat(Enumerable.Repeat(0, i - j - 1));
                    var allCertain = MultinomialDistribution.DensityAt(dirac, density);

                    Assert.Equal((double)1 / i, allCertain);
                }
            }
        }

        [Fact]
        public void MultinomialDistribution_Sum_FollowsMultinumialLaw()
        {
            var rand = new Random(0);
            const int N = 42;
            const int m = 4;
            const int nubmerOfIterations = 10;

            for (int i = 0; i < nubmerOfIterations; ++i)
            {
                double[] randomWeights = Enumerable.Range(0, m).Select(_ => rand.NextDouble()).ToArray();

                double sum = 0.0;
                for (int k1 = 0; k1 <= N; ++k1)
                {
                    for (int k2 = 0; k1 + k2 <= N; ++k2)
                    {
                        for (int k3 = 0; k1 + k2 + k3 <= N; ++k3)
                        {
                            int k4 = N - k1 - k2 - k3;
                            sum += MultinomialDistribution.DensityAt(new[] { k1, k2, k3, k4 }, randomWeights);
                        }
                    }
                }

                Assert.Equal(1.0, sum, precision: 4);
            }
        }
    }
}