using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.utils;

namespace PrincessProject.Princess.Strategy;

public class CurrentCandidatePositionAnalysisStrategy : IStrategy
{
    private IContenderChain _contenderChain;
    private int _contendersCount;
    public CurrentCandidatePositionAnalysisStrategy(IHall hall, int contendersCount)
    {
        _contendersCount = contendersCount;
        _contenderChain = new ContenderChainImpl(hall, contendersCount);
    }
    public bool AssessNextContender(ContenderName contender)
    {
        int position = _contenderChain.Add(contender);
        BigFloat probabilityOfSuccess = PrincessMath.CurrentCandidatePositionAnalysisStrategy(
            (uint)(_contenderChain.Size() - position - 1), 
            (uint)(position),
            (uint)(_contendersCount), 
            (uint)(_contendersCount * Constants.CurrentCandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryLowerBorderPercentage)
        );
        Console.WriteLine(probabilityOfSuccess);
        Console.WriteLine("POSITION IS:");
        Console.WriteLine(position);
        return probabilityOfSuccess >= Constants.CurrentCandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryProbability
               && _contenderChain.Size() >= _contendersCount * Constants.CurrentCandidatePositionAnalysisStrategyConfig.FirstContendersRejectedPercentage;
    }
}