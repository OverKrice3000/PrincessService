using Microsoft.Extensions.Hosting;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;

namespace PrincessProject;

public class PrincessService : IHostedService
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IHall _hall;
    private readonly IPrincess _princess;

    public PrincessService(IHostApplicationLifetime applicationLifetime, IPrincess princess, IHall hall)
    {
        _applicationLifetime = applicationLifetime;
        _princess = princess;
        _hall = hall;
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
        var happiness = _princess.ChooseHusband();
        _applicationLifetime.StopApplication();
    }
}