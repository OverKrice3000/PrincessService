using MassTransit;
using Nsu.PeakyBride.DataContracts;
using PrincessProject.utils;

namespace PrincessProject.consumers;

public class ContenderReceivedConsumer : IConsumer<Contender>
{
    private readonly EventContext _eventContext;

    public ContenderReceivedConsumer(EventContext eventContext)
    {
        _eventContext = eventContext;
    }

    public Task Consume(ConsumeContext<Contender> context)
    {
        var nextVisitingContender = Util.VisitingContenderFromFullName(context.Message.Name);
        _eventContext.InvokeCandidateReceived(nextVisitingContender);
        return Task.CompletedTask;
    }
}