using System.Numerics;
using PrincessProject.utils.PrincessMath;

namespace PrincessTestProject.UtilClassesTests.PrincessMathTests;

public class BigIntegerCacheOptimizedMathTests
{
    [Test]
    public void FactorialCalculationCorrectnessTest()
    {
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(0), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(0), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(1), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(1), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(5), Is.EqualTo(new BigInteger(120)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(5), Is.EqualTo(new BigInteger(120)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(10), Is.EqualTo(new BigInteger(3628800)));
        Assert.That(BigIntegerCache.CalculateFactorialCacheOptimized(10), Is.EqualTo(new BigInteger(3628800)));
    }

    [Test]
    public void BinomialCoefficientsCalculationCorrectnessTest()
    {
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(20, 20), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(20, 20), Is.EqualTo(new BigInteger(1)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(1, 20), Is.EqualTo(new BigInteger(0)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(1, 20), Is.EqualTo(new BigInteger(0)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(20, 1), Is.EqualTo(new BigInteger(20)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(20, 1), Is.EqualTo(new BigInteger(20)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(5, 3), Is.EqualTo(new BigInteger(10)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(5, 3), Is.EqualTo(new BigInteger(10)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(5, 2), Is.EqualTo(new BigInteger(10)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(5, 2), Is.EqualTo(new BigInteger(10)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(10, 2), Is.EqualTo(new BigInteger(45)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(10, 2), Is.EqualTo(new BigInteger(45)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(10, 3), Is.EqualTo(new BigInteger(120)));
        Assert.That(BigIntegerCache.CalculateBinomialCoefficientCacheOptimized(10, 3), Is.EqualTo(new BigInteger(120)));
    }
}