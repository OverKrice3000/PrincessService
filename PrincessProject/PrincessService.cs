using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.WorldGeneratorClasses;

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

    private async void Run()
    {
        Console.WriteLine("Running Activity!");
        await CalculateAverageHappiness();
        _applicationLifetime.StopApplication();
    }

    private async Task CalculateAverageHappiness()
    {
        using var scope = _scopeFactory.CreateScope();
        var generator =
            (FromDatabaseContenderGenerator)scope.ServiceProvider.GetRequiredService<IContenderGenerator>();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
        var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
        await worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
        double totalHappiness = 0;
        for (int i = 0; i < 100; i++)
        {
            generator.SetAttemptId(i);
            totalHappiness += princess.ChooseHusband();
        }

        Console.WriteLine(totalHappiness / 100);
    }

    private async Task ChooseHusbandForAttempt()
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
        var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
        await worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
        princess.ChooseHusband();
    }
}