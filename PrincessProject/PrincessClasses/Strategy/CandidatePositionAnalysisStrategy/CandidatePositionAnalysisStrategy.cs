using PrincessProject.Data.model;
using PrincessProject.utils;
using PrincessProject.utils.PrincessMath;
using radj307;

namespace PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;

public class CandidatePositionAnalysisStrategy : IStrategy
{
    private readonly IContenderChain _contenderChain;
    private readonly int _contendersCount;

    public CandidatePositionAnalysisStrategy(int attemptId)
    {
        _contendersCount = Data.Constants.DefaultContendersCount;
        _contenderChain = new ContenderChain(attemptId, _contendersCount);
    }

    public async Task<bool> AssessNextContender(VisitingContender visitingContender)
    {
        // Add contender to chain, receive his current position in the chain
        int position = await _contenderChain.Add(visitingContender);

        // Calculate how many contenders are better / worse than current one
        // And use those numbers to calculate probability that current contender
        // Is from top <N> of all the contenders
        BigFloat probabilityOfSuccess = PrincessMath.CurrentCandidatePositionAnalysisStrategyProbability(
            (uint)(_contenderChain.Size() - position - 1),
            (uint)(position),
            (uint)(_contendersCount),
            (uint)(_contendersCount *
                   CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryLowerBorderPercentage)
        );

        if (Constants.DebugMode)
        {
            Console.WriteLine(probabilityOfSuccess);
            Console.WriteLine("POSITION IS:");
            Console.WriteLine(position);
        }

        if (_contenderChain.Size() == _contendersCount &&
            position <= _contendersCount * Constants.IdiotHusbandTopBorderPercentage)
            return true;

        return probabilityOfSuccess >= CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryProbability
               && _contenderChain.Size() >= _contendersCount *
               CandidatePositionAnalysisStrategyConfig.FirstContendersRejectedPercentage;
    }
}