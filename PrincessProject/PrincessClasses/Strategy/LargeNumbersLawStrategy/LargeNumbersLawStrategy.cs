﻿using PrincessProject.Hall;
using PrincessProject.model;

namespace PrincessProject.PrincessClasses.Strategy.LargeNumbersLawStrategy;

public class LargeNumbersLawStrategy : IStrategy
{
    private readonly int _contendersCount;
    private readonly IContenderChain _contenderChain;
    private int _contendersAssessed = 0;

    public LargeNumbersLawStrategy(IHall hall, int contendersCount)
    {
        _contendersCount = contendersCount;
        _contenderChain = new ContenderChain(hall, contendersCount);
    }
    
    public bool AssessNextContender(ContenderName contender)
    {
        int contenderScore = _contenderChain.Add(contender);
        return _contendersAssessed++ >=
               _contendersCount * LargeNumbersLawStrategyConfig.FirstContendersRejectedPercentage
               && contenderScore <= LargeNumbersLawStrategyConfig.SatisfactoryContenderPositionUpperBorder;
    }
}