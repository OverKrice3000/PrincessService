using MassTransit;
using PrincessProject.Data.model;
using PrincessProject.Data.model.rabbitmq;
using PrincessProject.utils;

namespace PrincessProject.consumers;

public class ContenderConsumer : IConsumer<NextContenderMessage>
{
    public Task Consume(ConsumeContext<NextContenderMessage> context)
    {
        Console.WriteLine("CONSUMER");
        var nextVisitingContender = Util.VisitingContenderFromFullName(context.Message.Name);
        CandidateReceived?.Invoke(this, nextVisitingContender);
        return Task.CompletedTask;
    }

    public event EventHandler<VisitingContender> CandidateReceived;
}