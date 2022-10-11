using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.WorldGeneratorClasses;

namespace PrincessProject;

public class PrincessService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IAttemptSaver _attemptSaver;
    private readonly IContenderGenerator _generator;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IWorldGenerator _worldGenerator;

    public PrincessService(
        IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime applicationLifetime,
        IPrincess princess,
        IHall hall,
        IWorldGenerator worldGenerator
    )
    {
        _scopeFactory = scopeFactory;
        _applicationLifetime = applicationLifetime;
        _princess = princess;
        _hall = hall;
        var scope = scopeFactory
            .CreateScope();
        _generator = scope
            .ServiceProvider
            .GetServices<IContenderGenerator>()
            .First(o => o.GetType() == typeof(ContenderGenerator));
        _attemptSaver = scope
            .ServiceProvider
            .GetServices<IAttemptSaver>()
            .First(o => o.GetType() == typeof(DatabaseAttemptSaver));
        _worldGenerator = worldGenerator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting Application!");
        _applicationLifetime.ApplicationStarted.Register(Run);
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
        _worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
        var happiness = _princess.ChooseHusband();
        _applicationLifetime.StopApplication();
    }
}