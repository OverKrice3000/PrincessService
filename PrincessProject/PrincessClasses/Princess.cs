using PrincessProject.api;
using PrincessProject.Data.model;
using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using PrincessProject.PrincessClasses.Strategy.StatisticsCollectedAnalysisStrategy;
using PrincessProject.utils;

namespace PrincessProject.PrincessClasses;

public class Princess : IPrincess
{
    private int _attemptId = 0;
    private IStrategy _strategy = new CandidatePositionAnalysisStrategy(0);


    public Princess()
    {
    }

    public void SetAttemptId(int attemptId)
    {
        _attemptId = attemptId;
    }

    public async Task ResetAttempt()
    {
        await HallApi.ResetHall(_attemptId);
        _strategy = new CandidatePositionAnalysisStrategy(_attemptId);
    }

    public Task AskForNextContender()
    {
        return HallApi.NextContender(_attemptId);
    }

    public Task<bool> AssessNextContender(VisitingContender contender)
    {
        return _strategy.AssessNextContender(contender);
    }

    public async Task<int> SelectContenderAndCommentOnTopic(VisitingContender? chosen)
    {
        if (chosen is null)
        {
            Console.WriteLine("Princess hasn't chosen anyone!");
            Console.WriteLine("Her happiness level: {0}", Constants.NoHusbandHappinessLevel);
            return Constants.NoHusbandHappinessLevel;
        }

        int contenderValue = await HallApi.SelectContender(_attemptId);

        if (contenderValue == Constants.FirstContenderValue)
        {
            Console.WriteLine("Princess has chosen best husband: {0}", chosen.FullName);
            Console.WriteLine("Her happiness level: {0}", Constants.FirstHusbandHappinessLevel);
            return Constants.FirstHusbandHappinessLevel;
        }

        if (contenderValue == Constants.ThirdContenderValue)
        {
            Console.WriteLine("Princess has chosen third best husband: {0}", chosen.FullName);
            Console.WriteLine("Her happiness level: {0}", Constants.ThirdHusbandHappinessLevel);
            return Constants.ThirdHusbandHappinessLevel;
        }

        if (contenderValue == Constants.FifthContenderValue)
        {
            Console.WriteLine("Princess has chosen fifth best husband: {0}", chosen.FullName);
            Console.WriteLine("Her happiness level: {0}", Constants.FifthHusbandHappinessLevel);
            return Constants.FifthHusbandHappinessLevel;
        }

        Console.WriteLine("Princess has chosen an idiot husband: {0}", chosen.FullName);
        Console.WriteLine("Her happiness level: {0}", Constants.IdiotHusbandHappinessLevel);
        return Constants.IdiotHusbandHappinessLevel;
    }
}