using PrincessProject.api;
using PrincessProject.Data.model;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using PrincessProject.utils;

namespace PrincessProject.PrincessClasses;

public class Princess : IPrincess
{
    private int _attemptId = 0;
    private IStrategy? _strategy;

    public Princess()
    {
    }

    public void SetAttemptId(int attemptId)
    {
        _attemptId = attemptId;
    }

    public async Task<int> ChooseHusband()
    {
        await HallApi.ResetHall(_attemptId);
        int size = Data.Constants.DefaultContendersCount;
        _strategy = new CandidatePositionAnalysisStrategy(_attemptId);

        VisitingContender? chosen = null;
        for (int i = 0; i < size; i++)
        {
            VisitingContender nextVisitingContender = await HallApi.NextContender(_attemptId);
            if (await _strategy.AssessNextContender(nextVisitingContender))
            {
                chosen = nextVisitingContender;
                break;
            }
        }

        return await _calculateHappinessAndCommentOnTopic(chosen);
    }

    private async Task<int> _calculateHappinessAndCommentOnTopic(VisitingContender? chosen)
    {
        if (chosen is null)
        {
            Console.WriteLine("Princess hasn't chosen anyone!");
            Console.WriteLine("Her happiness level: {0}", Constants.NoHusbandHappinessLevel);
            return Constants.NoHusbandHappinessLevel;
        }

        int contenderValue = await HallApi.SelectContender(_attemptId);

        if (contenderValue <= Data.Constants.DefaultContendersCount * Constants.IdiotHusbandTopBorderPercentage)
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