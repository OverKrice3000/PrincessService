using System.Numerics;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using radj307;

namespace PrincessProject.utils.PrincessMath;

public static class PrincessMath
{
    public static BigInteger Factorial(uint n)
    {
        BigInteger result = 1;
        for (uint i = 1; i < n; i++)
        {
            result *= i + 1;
        }
        return result;
    }

    public static BigInteger BinomialCoefficient(uint n, uint m)
    {
        return Factorial(n) / Factorial(m) / Factorial(n - m);
    }

    // Probability, that given:
    // Total amount of candidates s
    // Amount of candidates, better, than candidate X
    // Amount of candidates, worse, than candidate X
    // Candidate X has score more than (lowerBorderL % * maximum score)
    public static BigFloat CurrentCandidatePositionAnalysisStrategy(uint n, uint m, uint s, uint lowerBorderL)
    {
        BigInteger[] cases = new BigInteger[s - n - m];
        for (uint i = 0; i <= s - m - n - 1; i++)
        {
            uint l = i + n + 1;
            cases[i] = BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(s - l, m)
                       * BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(l - 1, n);
            //cases[i] = BinomialCoefficient(s - l, m) * BinomialCoefficient(l - 1, n);
        }
        uint lowerBorderI = lowerBorderL - n - 1;
        BigInteger totalCases = 0;
        BigInteger fromLowerBorderCases = 0;
        for (uint i = 0; i <= s - m - n - 1; i++)
        {
            totalCases += cases[i];
            if(i >= lowerBorderI)
                fromLowerBorderCases += cases[i];
        }
        return new BigFloat(fromLowerBorderCases, totalCases);
    }

    public static BigFloat ProbabilityOfSuccess()
    {
        int s = Constants.DefaultContendersCount;
        BigFloat[] lChosenProbabilities = new BigFloat[100];
        BigFloat chosenProbability = new BigFloat(BigFloat.Zero);
        for (int n = (int)(s * CandidatePositionAnalysisStrategyConfig.FirstContendersRejectedPercentage) + 1;
             n <= 100;
             n++)
        {
            BigFloat nChosenProbability = new BigFloat(BigFloat.Zero);
            for (int l = 100; l >= 1; l--)
            {
                BigFloat lChosenProbablity = new BigFloat(BigFloat.Zero);
                for (int m = 1; m <= 100; m++)
                {
                    if (!CanHaveSuchPositionWithSuchValue(s, m, l))
                        continue;
                    if (!DoWeChooseContender(n, m, s))
                        continue;
                    BigFloat innerProbability = new BigFloat(
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)(l - 1), (uint)(n - m)) *
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)(m - 1), (uint)(n - 1)) *
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)(s - l), (uint)(m - 1)) *
                        BigIntegerCache.CalculateFactorialCacheOptimized((uint)(n - m)) *
                        BigIntegerCache.CalculateFactorialCacheOptimized((uint)(m - 1)),
                        BigIntegerCache.CalculateFactorialCacheOptimized((uint)n))
                        .Multiply(new BigFloat(BigFloat.One).Subtract(chosenProbability));
                    lChosenProbablity.Add(innerProbability);
                    Console.WriteLine(innerProbability);
                }

                lChosenProbabilities[l - 1].Add(lChosenProbablity);
                nChosenProbability.Add(lChosenProbablity);
            }

            chosenProbability.Add(nChosenProbability);
        }
        Console.WriteLine("L CHOSEN PROBABILITIES: ");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"L = {i}, PROB = {lChosenProbabilities[i]}");
        }
        Console.WriteLine("CHOSEN PROBABILITY {c}");
        return chosenProbability;
    }
    
    // n person's order
    // m his position 
    // l his number
    // contender has not yet been chosen
    public static bool DoWeChooseContender(int n, int m, int s)
    {
        return PrincessMath.CurrentCandidatePositionAnalysisStrategy(
            (uint)(n - m - 1),
            (uint)(m),
            (uint)(s),
            (uint)(s *
                   CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryLowerBorderPercentage)
        ) >= CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryProbability;
    }

    public static bool CanHaveSuchPositionWithSuchValue(int size, int m, int l)
    {
        return size - l + 1 <= m;
    }

    public static int ContendersBeforePosition(int n)
    {
        return n - 1;
    }

    public static int ContendersAfterPosition(int size, int n)
    {
        return size - n;
    }

    public static int ContendersWithLowerValue(int l)
    {
        return l - 1;
    }
    
    public static int ContendersWithHigherValue(int size, int l)
    {
        return size - l;
    }
}