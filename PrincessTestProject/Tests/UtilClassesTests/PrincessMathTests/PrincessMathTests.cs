using System.Numerics;
using PrincessProject.utils.PrincessMath;
using radj307;

namespace PrincessTestProject.UtilClassesTests.PrincessMathTests;

public class PrincessMathTests
{
    [Test]
    public void FactorialCalculationCorrectnessTest()
    {
        Assert.That(PrincessMath.Factorial(0), Is.EqualTo(new BigInteger(1)));
        Assert.That(PrincessMath.Factorial(1), Is.EqualTo(new BigInteger(1)));
        Assert.That(PrincessMath.Factorial(5), Is.EqualTo(new BigInteger(120)));
        Assert.That(PrincessMath.Factorial(10), Is.EqualTo(new BigInteger(3628800)));
    }

    [Test]
    public void BinomialCoefficientsCalculationCorrectnessTest()
    {
        Assert.That(PrincessMath.BinomialCoefficient(20, 20), Is.EqualTo(new BigInteger(1)));
        Assert.That(PrincessMath.BinomialCoefficient(20, 1), Is.EqualTo(new BigInteger(20)));
        Assert.That(PrincessMath.BinomialCoefficient(1, 20), Is.EqualTo(new BigInteger(0)));
        Assert.That(PrincessMath.BinomialCoefficient(5, 3), Is.EqualTo(new BigInteger(10)));
        Assert.That(PrincessMath.BinomialCoefficient(10, 2), Is.EqualTo(new BigInteger(45)));
    }

    [Test]
    public void CurrentCandidatePositionAnalysisStrategyProbabilityCalculationCorrectnessTest()
    {
        Assert.That(
            PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(2, 3, 20, 1),
            Is.GreaterThan(new BigFloat("0.99")));
        Assert.That(
            PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(2, 3, 20, 3),
            Is.GreaterThan(new BigFloat("0.99")));
        Assert.That(
            PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(1, 1, 5, 3),
            Is.EqualTo(new BigFloat("0.7")));
    }
}