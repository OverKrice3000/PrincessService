using System.Numerics;
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
        Console.WriteLine(s);
        Console.WriteLine(n);
        Console.WriteLine(m);
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
}