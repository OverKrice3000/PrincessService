using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.Princess;
using PrincessProject.Princess.Strategy;

namespace PrincessProject.PrincessClasses;

public class Princess : IPrincess
{
    private readonly IHall _hall;
    private IStrategy? _strategy;
    public Princess(IHall hall)
    {
        _hall = hall;
    }

    public Princess WithStrategy(IStrategy strategy)
    {
        _strategy = strategy;
        return this;
    }
    public ContenderName? MakeAssessment()
    {
        if (_strategy is null)
        {
            throw new ArgumentException("Strategy is not set!");
        }

        int size = _hall.GetTotalCandidates();
        for (int i = 0; i < size; i++)
        {
            ContenderName nextContender = _getNextContender();
            if (_strategy.AssessNextContender(nextContender) == true)
                return nextContender;
        }

        return null;
    }

    private ContenderName _getNextContender()
    {
        return _hall.GetNextContender();
    }
    
    
}