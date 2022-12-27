using MassTransit;
using PrincessProject.Data.model.rabbitmq;
using PrincessProject.utils;

namespace PrincessProject.consumers;

public class ContenderReceivedConsumer : IConsumer<NextContenderMessage>
{
    private readonly EventContext _eventContext;

    public ContenderReceivedConsumer(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public Task Consume(ConsumeContext<NextContenderMessage> context)
    {
        var nextVisitingContender = Util.VisitingContenderFromFullName(context.Message.Name);
        _eventContext.InvokeCandidateReceived(nextVisitingContender);
        return Task.CompletedTask;
    }
}