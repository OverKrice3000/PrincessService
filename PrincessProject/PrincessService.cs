using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;

namespace PrincessProject;

public class PrincessService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IContenderGenerator _generator;
    private readonly IHall _hall;
    private readonly IPrincess _princess;
    private readonly IServiceScopeFactory _scopeFactory;

    public PrincessService(
        IServiceScopeFactory scopeFactory,
        IHostApplicationLifetime applicationLifetime,
        IContenderGenerator generator,
        IPrincess princess,
        IHall hall
    )
    {
        _scopeFactory = scopeFactory;
        _applicationLifetime = applicationLifetime;
        _princess = princess;
        _hall = hall;
        _generator = scopeFactory
            .CreateScope()
            .ServiceProvider
            .GetServices<IContenderGenerator>()
            .First(o => o.GetType() == typeof(ContenderGenerator));
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
            var contenders = _generator.Generate(Constants.DefaultContendersCount);
            foreach (var tuple in contenders.Select((contender, index) => (contender, index)))
            {
                context.Attempts.Add(
                    new AttemptDto()
                    {
                        AttemptId = i,
                        CandidateName = tuple.contender.Name,
                        CandidateSurname = tuple.contender.Surname,
                        CandidateValue = tuple.contender.Value,
                        CandidateOrder = tuple.index
                    }
                );
            }
        }

        context.SaveChanges();
    }
}