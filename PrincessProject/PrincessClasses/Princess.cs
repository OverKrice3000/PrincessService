using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.Princess;
using PrincessProject.Princess.Strategy;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.utils;

namespace PrincessProject.PrincessClasses;

public class Princess : IPrincess
{
    private readonly IHall _hall;
    private IStrategy? _strategy;

    public Princess(IHall hall)
    {
        _hall = hall;
    }

    public ContenderName? ChooseHusband()
    {
        int size = _hall.GetTotalCandidates();
        _strategy = new CandidatePositionAnalysisStrategy(_hall);

        for (int i = 0; i < size; i++)
        {
            ContenderName nextContender = _hall.GetNextContender();
            if (_strategy.AssessNextContender(nextContender) == true)
                return nextContender;
        }

        return null;
    }
}