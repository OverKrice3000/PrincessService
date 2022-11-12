using MassTransit;
using PrincessProject.api;
using PrincessProject.Data.model;
using PrincessProject.Data.model.rabbitmq;
using PrincessProject.PrincessClasses.Strategy;
using PrincessProject.PrincessClasses.Strategy.CandidatePositionAnalysisStrategy;
using PrincessProject.utils;

namespace PrincessProject.PrincessClasses;

public class Princess : IPrincess, IConsumer<NextContenderMessage>
{
    private int _attemptId = 0;
    private IStrategy _strategy = new CandidatePositionAnalysisStrategy(0);

    public Princess()
    {
    }

    public async Task Consume(ConsumeContext<NextContenderMessage> context)
    {
        Console.WriteLine("CONSUMER");
        var nextVisitingContender = Util.VisitingContenderFromFullName(context.Message.Name);
        var isChosen = (await _strategy.AssessNextContender(nextVisitingContender));
        lock (this)
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                Monitor.Pulse(this);
            }
        }
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
            lock (this)
            {
                HallApi.NextContender(_attemptId);
                Console.WriteLine("SENT");
                Monitor.Wait(this);
            }

            Console.WriteLine("AWAITED");
            if (chosen is not null)
            {
                break;
            }
        }

        return await _calculateHappinessAndCommentOnTopic(new VisitingContender("kek", "lol"));
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