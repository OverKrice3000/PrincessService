using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.api;
using PrincessProject.consumers;
using PrincessProject.Data;
using PrincessProject.Data.model;
using PrincessProject.PrincessClasses;

namespace PrincessProject;

public class PrincessService : IHostedService
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
        DoAllAttempts();
    }

    private void DoAllAttempts()
    {
        HallApi.ResetHall().Wait();

        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();

        const int contendersCount = Constants.DefaultContendersCount;
        int currentContender = 0;
        int currentAttempt = 0;

        void OnStartAction(int attemptId)
        {
            currentContender = 0;
            try
            {
                princess.SetAttemptId(attemptId);
                princess.ResetAttempt();
                princess.AskForNextContender();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception occured");
                Console.WriteLine(e);
                _applicationLifetime.StopApplication();
            }
        }

        async void OnCandidateReceivedAction(VisitingContender contender)
        {
            currentContender++;
            try
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
                    await princess.AskForNextContender();
                    await princess.SelectContenderAndCommentOnTopic(null);
                    _eventContext.InvokeFinishAttempt();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Unexpected exception occured");
                _applicationLifetime.StopApplication();
            }
        }

        void OnAttemptFinishAction()
        {
            if (++currentAttempt < Constants.DatabaseAttemptsGenerated)
            {
                _eventContext.InvokeStartAttempt(currentAttempt);
            }
            else
            {
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
            try
            {
                princess.SetAttemptId(attemptId);
                princess.ResetAttempt();
                princess.AskForNextContender();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception occured");
                Console.WriteLine(e);
                _applicationLifetime.StopApplication();
            }
        }

        async void OnCandidateReceivedAction(VisitingContender contender)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Unexpected exception occured");
                _applicationLifetime.StopApplication();
            }
        }

        void OnAttemptFinishAction()
        {
            if (++currentAttempt < Constants.DatabaseAttemptsGenerated)
            {
                _eventContext.InvokeStartAttempt(currentAttempt);
            }
            else
            {
                Console.WriteLine(
                    $"Princess average happiness is {totalHappiness / Constants.DatabaseAttemptsGenerated}");
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

    private Task ChooseHusbandForAttempt(int attemptId)
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();

        const int contendersCount = Constants.DefaultContendersCount;
        int currentContender = 0;

        void OnStartAction(int attemptId)
        {
            try
            {
                princess.SetAttemptId(attemptId);
                princess.ResetAttempt();
                princess.AskForNextContender();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Unexpected exception occured");
                _applicationLifetime.StopApplication();
            }
        }

        async void OnCandidateReceivedAction(VisitingContender contender)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Unexpected exception occured");
                _applicationLifetime.StopApplication();
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
        return Task.CompletedTask;
    }
}