using radj307;

namespace PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;

public static class CandidatePositionAnalysisStrategyConfig
{
    public const double FirstContendersRejectedPercentage = 0.4;
    public const double WorthyContenderSatisfactoryLowerBorderPercentage = 0.94;
    public static readonly BigFloat WorthyContenderSatisfactoryProbability = new BigFloat("0.85");
}