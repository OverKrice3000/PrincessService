using System.Numerics;
using FluentAssertions;
using PrincessProject.utils.PrincessMath;
using radj307;

namespace PrincessTestProject.UtilClassesTests.PrincessMathTests;

public class PrincessMathTests
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

    static object[] ProbabilityCorrectCalculationCases =
    {
        new object[] { (uint)1, (uint)1, (uint)5, (uint)3, new BigFloat("0.7") },
    };

    static object[] ProbabilitySumsToOneCases =
    {
        new object[] { (uint)2, (uint)3, (uint)20, (uint)1 },
        new object[] { (uint)2, (uint)3, (uint)20, (uint)3 },
    };

    [
        TestCaseSource(nameof(FactorialCases)),
    ]
    public void ShouldCalculateFactorialsCorrectly(in uint input, in BigInteger expected)
    {
        expected.Should().Be(PrincessMath.Factorial(input));
    }

    [TestCaseSource(nameof(BinomialCoefficientCases))]
    public void ShouldCalculateBinomialCoefficientsCorrectly(in uint n, in uint m, in BigInteger expected)
    {
        expected.Should().Be(PrincessMath.BinomialCoefficient(n, m));
    }

    [TestCaseSource(nameof(ProbabilityCorrectCalculationCases))]
    public void ShouldCalculateStrategyProbabilityCorrectly(in uint n, in uint m,
        in uint s, in uint lowerBorderL, in BigFloat expected)
    {
        expected.Should().Be(PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(n, m, s, lowerBorderL));
    }

    [TestCaseSource(nameof(ProbabilitySumsToOneCases))]
    public void StrategyProbabilityShouldSumToOne(in uint n, in uint m, in uint s, in uint lowerBorderL)
    {
        PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(n, m, s, lowerBorderL).Should()
            .BeGreaterThan(new BigFloat("0.99"));
    }
}