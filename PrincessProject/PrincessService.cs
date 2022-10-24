using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

    private void Run()
    {
        Console.WriteLine("Running Activity!");
        using (var scope = _scopeFactory.CreateScope())
        {
            var princess = scope.ServiceProvider.GetRequiredService<IPrincess>();
            var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
            worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
            princess.ChooseHusband();
        }

        _applicationLifetime.StopApplication();
    }
}