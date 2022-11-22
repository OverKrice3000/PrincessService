using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.consumers;
using PrincessProject.Data;
using PrincessProject.PrincessClasses;

namespace PrincessProject;

public class PrincessService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IServiceScopeFactory _scopeFactory;

    public PrincessService(
        IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime applicationLifetime
    )
    {
        _scopeFactory = scopeFactory;
        _applicationLifetime = applicationLifetime;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting Application!");
        Task.Run(Run);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping Application!");
        return Task.CompletedTask;
    }

    private event Action<int> OnStartAttempt;
    private event Action OnFinishAttempt;

    private async void Run()
    {
        Console.WriteLine("Running Activity!");
        await CalculateAverageHappiness();
        _applicationLifetime.StopApplication();
    }

    private async Task CalculateAverageHappiness()
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
        var contenderConsumer = scope.ServiceProvider.GetRequiredService<ContenderConsumer>();

        const int contendersCount = Constants.DefaultContendersCount;
        int currentContender = 0;
        int currentAttempt = 0;

        double totalHappiness = 0;

        OnStartAttempt += (attemptId) =>
        {
            princess.SetAttemptId(attemptId);
            princess.ResetAttempt();
            princess.AskForNextContender();
        };

        contenderConsumer.CandidateReceived += async (sender, contender) =>
        {
            var isChosen = await princess.AssessNextContender(contender);
            if (isChosen)
            {
                totalHappiness += await princess.SelectContenderAndCommentOnTopic(contender);
                OnFinishAttempt.Invoke();
            }
            else if (currentContender < contendersCount)
            {
                await princess.AskForNextContender();
            }
            else
            {
                OnFinishAttempt.Invoke();
            }
        };

        OnFinishAttempt += () =>
        {
            if (currentAttempt < 100)
            {
                OnStartAttempt.Invoke(++currentAttempt);
            }
            else
            {
                Console.WriteLine(totalHappiness / 100);
            }
        };

        OnStartAttempt.Invoke(currentAttempt);
    }

    private async Task ChooseHusbandForAttempt(int attemptId)
    {
        /*using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
        princess.SetAttemptId(attemptId);
        await princess.ChooseHusband();*/
    }
}