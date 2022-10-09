using System.Numerics;
using FluentAssertions;
using PrincessProject.utils.PrincessMath;

namespace PrincessTestProject.UtilClassesTests.PrincessMathTests;

/// <summary>
/// This class tests math methods of BigIntegerCache, which caches results of calculations
/// This means, that in every test every calculation should be done at least twice
/// First time cache does calculation by itself
/// Second time it returns value from cache
/// </summary>
public class BigIntegerCacheOptimizedMathTests
{
    static object[] FactorialCases =
    {
        new object[] { (uint)0, new BigInteger(1) },
        new object[] { (uint)1, new BigInteger(1) },
        new object[] { (uint)5, new BigInteger(120) },
        new object[] { (uint)10, new BigInteger(3628800) },
    };

    static object[] BinomialCoefficientCases =
    {
        new object[] { (uint)20, (uint)20, new BigInteger(1) },
        new object[] { (uint)20, (uint)1, new BigInteger(20) },
        new object[] { (uint)1, (uint)20, new BigInteger(0) },
        new object[] { (uint)5, (uint)3, new BigInteger(10) },
        new object[] { (uint)10, (uint)2, new BigInteger(45) },
    };

    [TestCaseSource(nameof(FactorialCases))]
    public void ShouldCalculateFactorialsCorrectly(in uint input, in BigInteger expected)
    {
        expected.Should().Be(BigIntegerCache.CalculateFactorialCacheOptimized(input));
        expected.Should().Be(BigIntegerCache.CalculateFactorialCacheOptimized(input));
    }

    [TestCaseSource(nameof(BinomialCoefficientCases))]
    public void ShouldCalculateBinomialCoefficientsCorrectly(in uint n, in uint m, in BigInteger expected)
    {
        expected.Should().Be(PrincessMath.BinomialCoefficient(n, m));
        expected.Should().Be(PrincessMath.BinomialCoefficient(n, m));
    }
}