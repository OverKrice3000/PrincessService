using System.Globalization;
using System.Numerics;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using radj307;

namespace PrincessProject.utils.PrincessMath;

public static class QualityMetrics
{
    public static BigFloat ProbabilityOfSuccess()
    {
        int s = Constants.DefaultContendersCount;
        BigFloat[] lChosenProbabilities = new BigFloat[100];
        for (int l = 0; l < 100; l++)
        {
            lChosenProbabilities[l] = new BigFloat(BigFloat.Zero);
        }

        BigFloat notChosenProbability = new BigFloat(BigFloat.One);
        for (int n = (int)(s *
                           CandidatePositionAnalysisStrategyConfig.FirstContendersRejectedPercentage) + 1;
             n < 100;
             n++)
        {
            Console.WriteLine(n);
            Console.WriteLine($"NOT CHOSEN PROBABILITY {notChosenProbability}");
            BigFloat nChosenProbability = new BigFloat(BigFloat.Zero);
            for (int l = 100; l >= 1; l--)
            {
                BigFloat lChosenProbability = new BigFloat(0, 1);
                for (int m = 1; m <= 100; m++)
                {
                    if (!CanHaveSuchPositionWithSuchValue(s, n, m, l))
                        continue;
                    if (!DoWeChooseContender(n, m, s))
                    {
                        continue;
                    }

                    BigFloat innerProbability = new BigFloat(
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)(l - 1), (uint)(n - m)) *
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)(s - l), (uint)(m - 1))
                        ,
                        new BigInteger(n) *
                        BigIntegerCache.CalculateBinomialCoefficientCacheOptimized((uint)s, (uint)n));
                    lChosenProbability.Add(innerProbability);
                }

                nChosenProbability.Add(lChosenProbability);
                lChosenProbabilities[l - 1].Add(lChosenProbability.Multiply(notChosenProbability));
            }

            notChosenProbability.Multiply(new BigFloat(BigFloat.One).Subtract(nChosenProbability));
        }

        BigFloat n100ChosenProbability = new BigFloat(BigFloat.Zero);
        for (int l = 51; l <= 100; l++)
        {
            BigFloat lChosenProbability = new BigFloat(1, s);
            n100ChosenProbability.Add(lChosenProbability);
            lChosenProbabilities[l - 1].Add(lChosenProbability.Multiply(notChosenProbability));
        }

        notChosenProbability.Multiply(new BigFloat(BigFloat.One).Subtract(n100ChosenProbability));
        var lChosenProbabilitiesString = new string[s];
        var lChosenProbabilitiesDouble = new decimal[s];
        Console.WriteLine("L CHOSEN PROBABILITIES: ");
        for (int i = 0; i < 100; i++)
        {
            lChosenProbabilitiesString[i] = lChosenProbabilities[i].ToString();
            lChosenProbabilitiesDouble[i] = decimal.Parse(lChosenProbabilitiesString[i], CultureInfo.InvariantCulture);
        }

        Console.WriteLine($"NOT CHOSEN PROBABILITY {notChosenProbability}");
        for (int i = 98; i >= 0; i--)
        {
            if (lChosenProbabilitiesDouble[i + 1] < lChosenProbabilitiesDouble[i])
            {
                for (int j = i; j >= 0; j--)
                {
                    lChosenProbabilitiesDouble[j] /= 10;
                }
            }
        }

        while (true)
        {
            decimal sumOfProbabilities = 0;
            for (int i = 0; i < 100; i++)
            {
                sumOfProbabilities += lChosenProbabilitiesDouble[i];
            }

            Console.WriteLine(sumOfProbabilities);

            if (sumOfProbabilities > 1)
            {
                for (int i = 0; i < 100; i++)
                {
                    lChosenProbabilitiesDouble[i] /= 10;
                }

                continue;
            }

            break;
        }

        decimal chosenProbability = 0;
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine($"L = {i + 1}, PROB = {lChosenProbabilitiesDouble[i]}");
            chosenProbability += lChosenProbabilitiesDouble[i];
        }

        Console.WriteLine($"TOTAL PROBABILITY {chosenProbability}");
        decimal averageScore = 0;
        for (int i = 50; i < 100; i++)
        {
            averageScore += (i + 1) * lChosenProbabilitiesDouble[i];
        }

        averageScore += 10 * (1 - chosenProbability);
        Console.WriteLine($"AVERAGE CONTENDER SCORE {averageScore}");

        return BigFloat.One;
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

    public static bool CanHaveSuchPositionWithSuchValue(int size, int n, int m, int l)
    {
        return m <= n && size - l + 1 >= m && n - l + 1 <= m;
    }
}