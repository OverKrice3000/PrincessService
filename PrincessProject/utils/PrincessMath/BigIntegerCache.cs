using System.Numerics;

namespace PrincessProject.utils.PrincessMath;

public static class BigIntegerCache
{
    private record BinomialCoefficientInput(uint n, uint m);
    
    private static Dictionary<uint, BigInteger> _factorialsCached = new();
    private static Dictionary<uint, SortedDictionary<int, BigInteger>> _binomialCoefficientsCached = new();

    static BigIntegerCache()
    {
        _factorialsCached.Add(0, 1);
        _factorialsCached.Add(1, 1);
        _factorialsCached.Add(2, 2);
        _factorialsCached.Add(3, 6);
    }

    private static uint _findClosestFactorialCached(uint n)
    {
        if (_factorialsCached.ContainsKey(n))
            return n;
        uint leftBorder = 1;
        uint rightBorder = n - 1;
        while (leftBorder < rightBorder)
        {
            if (_factorialsCached.ContainsKey((leftBorder + rightBorder) / 2 + 1))
                leftBorder = (leftBorder + rightBorder) / 2 + 1;
            else
                rightBorder = (leftBorder + rightBorder) / 2 ;
        }

        return leftBorder;
    }

    private static int _findClosestBinomialCoefficientCached(uint n, uint m)
    {
        if (_binomialCoefficientsCached[n].ContainsKey((int)m))
            return (int)m;
        else
            return _binomialCoefficientsCached[n].Keys.ElementAt(_binomialCoefficientsCached[n].Count - 1);
    }
    
    public static BigInteger CalculateFactorialCacheOptimized(uint n)
    {
        uint closestFactorialCachedKey = _findClosestFactorialCached(n);
        BigInteger currentFactorial = _factorialsCached[closestFactorialCachedKey];
        for (uint i = closestFactorialCachedKey; i < n; i++)
        {
            currentFactorial = BigInteger.Multiply(currentFactorial, i + 1);
            _factorialsCached.Add(i + 1, currentFactorial);
        }
        return currentFactorial;
    }

    public static BigInteger CalculateBinomialCoefficientCacheOptimized(uint n, uint m)
    {
        if (n == m)
        {
            return BigInteger.One;
        }
        if (!_binomialCoefficientsCached.ContainsKey(n))
        {
            _binomialCoefficientsCached.Add(n, new SortedDictionary<int, BigInteger>());
            BigInteger binomialCoefficient = CalculateFactorialCacheOptimized(n) 
                                             / CalculateFactorialCacheOptimized(m) 
                                             / CalculateFactorialCacheOptimized(n - m);
            _binomialCoefficientsCached[n].Add((int)m, binomialCoefficient);
            return binomialCoefficient;
        }
        int closestM = _findClosestBinomialCoefficientCached(n, m);
        BigInteger nextBinomialCoefficient = _binomialCoefficientsCached[n][closestM];
        if (closestM == m)
        {
            return nextBinomialCoefficient;
        }
        else if (closestM < m)
        {
            while (closestM != m)
            {
                nextBinomialCoefficient = BigInteger.Multiply(nextBinomialCoefficient, n - closestM) / (closestM + 1);
                closestM++;
                _binomialCoefficientsCached[n].TryAdd(closestM, nextBinomialCoefficient);
            }
            return nextBinomialCoefficient;
        }
        else
        {
            while (closestM != m)
            {
                nextBinomialCoefficient = BigInteger.Multiply(nextBinomialCoefficient,  closestM ) / (n - closestM + 1);
                closestM--;
                _binomialCoefficientsCached[n].TryAdd(closestM, nextBinomialCoefficient);
            }
            return nextBinomialCoefficient;
        }
    }
}