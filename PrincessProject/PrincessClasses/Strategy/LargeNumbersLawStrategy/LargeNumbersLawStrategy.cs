using PrincessProject.Data.model;

namespace PrincessProject.PrincessClasses.Strategy.LargeNumbersLawStrategy;

public class LargeNumbersLawStrategy : IStrategy
{
    private readonly IContenderChain _contenderChain;
    private readonly int _contendersCount;
    private int _contendersAssessed = 0;

    public LargeNumbersLawStrategy(int attemptId)
    {
        _contendersCount = Data.Constants.DefaultContendersCount;
        _contenderChain = new ContenderChain(attemptId, _contendersCount);
    }

    public async Task<bool> AssessNextContender(VisitingContender visitingContender)
    {
        int contenderScore = await _contenderChain.Add(visitingContender);
        return _contendersAssessed++ >=
               _contendersCount * LargeNumbersLawStrategyConfig.FirstContendersRejectedPercentage
               && contenderScore <= LargeNumbersLawStrategyConfig.SatisfactoryContenderPositionUpperBorder;
    }
}