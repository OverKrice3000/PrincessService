using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.Hall;
using PrincessProject.model;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;

namespace PrincessProject;

public class PrincessService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IAttemptSaver _attemptSaver;
    private readonly IContenderGenerator _generator;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IServiceScopeFactory _scopeFactory;

    public PrincessService(
        IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime applicationLifetime,
        IPrincess princess,
        IHall hall
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
        EnsureAttemptsInDatabase();
        var happiness = _princess.ChooseHusband();
        _applicationLifetime.StopApplication();
    }

    private void EnsureAttemptsInDatabase()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AttemptContext>();
        if (context.Attempts.Any())
        {
            return;
        }

        for (int i = 0; i < Constants.DatabaseAttemptsGenerated; i++)
        {
            var contenders = _generator.Generate(Constants.DefaultContendersCount)
                .Select(c => new ContenderData(c.Name, c.Surname, c.Value)).ToArray();
            foreach (var tuple in contenders.Select((contender, index) => (contender, index)))
            {
                _attemptSaver.Save(new Attempt(contenders.Length, contenders, null));
            }
        }

        context.SaveChanges();
    }
}