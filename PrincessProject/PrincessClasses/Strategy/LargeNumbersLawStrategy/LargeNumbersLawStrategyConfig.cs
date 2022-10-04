namespace PrincessProject.PrincessClasses.Strategy.LargeNumbersLawStrategy;

public class LargeNumbersLawStrategyConfig
{
    /// <summary>
    /// Percentage of candidates, which are immediately rejected
    /// </summary>
    public const double FirstContendersRejectedPercentage = 1 / Math.E;

    /// <summary>
    /// Maximum satisfactory position in contender chain
    /// </summary>
    public const int SatisfactoryContenderPositionUpperBorder = 8;
}