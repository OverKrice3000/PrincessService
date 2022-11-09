using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        double totalHappiness = 0;
        for (int i = 0; i < 100; i++)
        {
            totalHappiness += await princess.ChooseHusband();
        }

        Console.WriteLine(totalHappiness / 100);
    }

    //TODO add attempt
    private void ChooseHusbandForAttempt()
    {
        using var scope = _scopeFactory.CreateScope();
        var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
        princess.ChooseHusband();
    }
}