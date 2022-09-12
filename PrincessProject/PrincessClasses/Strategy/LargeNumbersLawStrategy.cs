using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.Princess.Strategy;

public class LargeNumbersLawStrategy : IStrategy
{
    private readonly int _contendersCount;
    private readonly IContenderChain _contenderChain;
    private int _contendersAssessed = 0;

    public LargeNumbersLawStrategy(IHall hall, int contendersCount)
    {
        _contendersCount = contendersCount;
        _contenderChain = new ContenderChainImpl(hall, contendersCount);
    }
    public bool AssessNextContender(ContenderName contender)
    {
        int contenderScore = _contenderChain.Add(contender);
        if (
            _contendersAssessed++ >= _contendersCount * Constants.LargeNumbersLawStrategyConfig.FirstContendersRejectedPercentage
            && contenderScore <= Constants.LargeNumbersLawStrategyConfig.SatisfactoryContenderPositionUpperBorder
        )
        {
            return true;
        }

        return false;
    }
}