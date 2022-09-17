using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using PrincessProject.utils;
using PrincessProject.utils.PrincessMath;
using radj307;

namespace PrincessProject.Princess.Strategy;

public class CandidatePositionAnalysisStrategy : IStrategy
{
    private readonly IContenderChain _contenderChain;
    private readonly int _contendersCount;
    
    public CandidatePositionAnalysisStrategy(IHall hall)
    {
        _contendersCount = hall.GetTotalCandidates();
        _contenderChain = new ContenderChain(hall, _contendersCount);
    }
    
    public bool AssessNextContender(ContenderName contender)
    {
        // Add contender to chain, receive his current position in the chain
        int position = _contenderChain.Add(contender);
        
        // Calculate how many contenders are better / worse than current one
        // And use those numbers to calculate probability that current contender
        // Is from top <N> of all the contenders
        BigFloat probabilityOfSuccess = PrincessMath.CurrentCandidatePositionAnalysisStrategy(
            (uint)(_contenderChain.Size() - position - 1), 
            (uint)(position),
            (uint)(_contendersCount), 
            (uint)(_contendersCount * CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryLowerBorderPercentage)
        );
        
        if (Constants.DebugMode)
        {
            Console.WriteLine(probabilityOfSuccess);
            Console.WriteLine("POSITION IS:");
            Console.WriteLine(position);
        }
        return probabilityOfSuccess >= CandidatePositionAnalysisStrategyConfig.WorthyContenderSatisfactoryProbability
               && _contenderChain.Size() >= _contendersCount * CandidatePositionAnalysisStrategyConfig.FirstContendersRejectedPercentage;
    }
}