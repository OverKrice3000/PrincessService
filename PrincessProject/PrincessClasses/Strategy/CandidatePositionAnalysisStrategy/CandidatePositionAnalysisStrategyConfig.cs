using PrincessProject.utils;
using radj307;

namespace PrincessProject.Princess.Strategy;

public static class CandidatePositionAnalysisStrategyConfig
{
    public const double FirstContendersRejectedPercentage = 0.1;
    public const double WorthyContenderSatisfactoryLowerBorderPercentage = 0.95;
    public static readonly BigFloat WorthyContenderSatisfactoryProbability = new BigFloat("0.9");
}