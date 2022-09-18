using PrincessProject.Hall;
using PrincessProject.model;
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

    public int ChooseHusband()
    {
        int size = _hall.GetTotalCandidates();
        _strategy = new CandidatePositionAnalysisStrategy(_hall);

        VisitingContender? chosen = null;
        for (int i = 0; i < size; i++)
        {
            VisitingContender nextVisitingContender = _hall.GetNextContender();
            if (_strategy.AssessNextContender(nextVisitingContender))
            {
                chosen = nextVisitingContender;
                break;
            }
        }

        if (chosen is null)
        {
            Console.WriteLine("Princess hasn't chosen anyone!");
            Console.WriteLine("Her happiness level: {0}", Constants.NoHusbandHappinessLevel);
            return Constants.NoHusbandHappinessLevel;
        }

        int contenderValue = _hall.ChooseContender(chosen);

        if (contenderValue <= size * Constants.IdiotHusbandTopBorderPercentage)
        {
            Console.WriteLine("Princess has chosen an idiot husband: {0}", chosen.FullName);
            Console.WriteLine("Her happiness level: {0}", Constants.IdiotHusbandHappinessLevel);
            return Constants.IdiotHusbandHappinessLevel;
        }

        Console.WriteLine("Princess has chosen a worthy contender: {0}", chosen.FullName);
        Console.WriteLine("Her happiness level: {0}", contenderValue);
        return contenderValue;
    }
}