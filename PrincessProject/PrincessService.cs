using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.consumers;
using PrincessProject.Data.model;
using PrincessProject.Data.model.rabbitmq;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using Constants = PrincessProject.Data.Constants;

namespace PrincessProject;

public class PrincessService : IHostedService, IConsumer<NextContenderMessage>
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly EventContext _eventContext;
    private readonly IServiceScopeFactory _scopeFactory;

    public PrincessService(
        IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime applicationLifetime,
        EventContext eventContext
    )
    {
        _eventContext = eventContext;
        _scopeFactory = scopeFactory;
        _applicationLifetime = applicationLifetime;
    }

    public Task Consume(ConsumeContext<NextContenderMessage> context)
    {
        var nextVisitingContender = Util.VisitingContenderFromFullName(context.Message.Name);
        _eventContext.InvokeCandidateReceived(nextVisitingContender);
        return Task.CompletedTask;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(Run);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }


    private void Run()
    {
        CalculateAverageHappiness();
    }

    private void CalculateAverageHappiness()
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();

        const int contendersCount = Constants.DefaultContendersCount;
        int currentContender = 0;
        int currentAttempt = 0;

        double totalHappiness = 0;

        void OnStartAction(int attemptId)
        {
            princess.SetAttemptId(attemptId);
            princess.ResetAttempt();
            princess.AskForNextContender();
        }

        async void OnCandidateReceivedAction(VisitingContender contender)
        {
            var isChosen = await princess.AssessNextContender(contender);
            if (isChosen)
            {
                totalHappiness += await princess.SelectContenderAndCommentOnTopic(contender);
                _eventContext.InvokeFinishAttempt();
            }
            else if (currentContender < contendersCount)
            {
                await princess.AskForNextContender();
            }
            else
            {
                totalHappiness += await princess.SelectContenderAndCommentOnTopic(null);
                _eventContext.InvokeFinishAttempt();
            }
        }

        void OnAttemptFinishAction()
        {
            if (++currentAttempt < 100)
            {
                _eventContext.InvokeStartAttempt(currentAttempt);
            }
            else
            {
                Console.WriteLine($"Princess average happiness is {totalHappiness / 100}");
                _eventContext.OnStartAttempt -= OnStartAction;
                _eventContext.OnCandidateReceived -= OnCandidateReceivedAction;
                _eventContext.OnFinishAttempt -= OnAttemptFinishAction;
                _applicationLifetime.StopApplication();
            }
        }

        _eventContext.OnStartAttempt += OnStartAction;
        _eventContext.OnCandidateReceived += OnCandidateReceivedAction;
        _eventContext.OnFinishAttempt += OnAttemptFinishAction;
        _eventContext.InvokeStartAttempt(currentAttempt);
    }

    private async Task ChooseHusbandForAttempt(int attemptId)
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();

        const int contendersCount = Constants.DefaultContendersCount;
        int currentContender = 0;

        void OnStartAction(int attemptId)
        {
            princess.SetAttemptId(attemptId);
            princess.ResetAttempt();
            princess.AskForNextContender();
        }

        async void OnCandidateReceivedAction(VisitingContender contender)
        {
            var isChosen = await princess.AssessNextContender(contender);
            if (isChosen)
            {
                await princess.SelectContenderAndCommentOnTopic(contender);
                _eventContext.InvokeFinishAttempt();
            }
            else if (currentContender < contendersCount)
            {
                await princess.AskForNextContender();
            }
            else
            {
                await princess.SelectContenderAndCommentOnTopic(null);
                _eventContext.InvokeFinishAttempt();
            }
        }

        void OnAttemptFinishAction()
        {
            _eventContext.OnStartAttempt -= OnStartAction;
            _eventContext.OnCandidateReceived -= OnCandidateReceivedAction;
            _eventContext.OnFinishAttempt -= OnAttemptFinishAction;
            _applicationLifetime.StopApplication();
        }

        _eventContext.OnStartAttempt += OnStartAction;
        _eventContext.OnCandidateReceived += OnCandidateReceivedAction;
        _eventContext.OnFinishAttempt += OnAttemptFinishAction;
        _eventContext.InvokeStartAttempt(attemptId);
    }
}