using HallWeb.ContenderContainer;
using Microsoft.Extensions.Configuration;
using PrincessProject.Data.model;

namespace PrincessTestProject.Mocks.ContenderContainer;

/// <summary>
/// Mock implementation of IContenderContainer, which generates contenders with unique
/// names and surnames. i-th contender in the container has i value.
/// </summary>
public class MContenderContainer : IContenderContainer
{
    private IConfigurationRoot _config;
    private Contender[] _contenders;
    private AttemptContainerContext _context;

    public MContenderContainer(in int size)
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        _contenders = _generateContenders(size);
        _context = new AttemptContainerContext(_contenders);
        Container = new Dictionary<int, AttemptContainerContext>();
    }

    public Dictionary<int, AttemptContainerContext> Container { get; }

    public AttemptContainerContext this[int attemptId] => _context;

    public Contender FindContenderByName(
        int attemptId,
        VisitingContender visitingContender
    )
    {
        return Array.Find(
                   _context.Contenders,
                   contender => contender.FullName.Equals(visitingContender.FullName)
               ) ??
               throw new ArgumentException(_config.GetSection("ExceptionMessages")["MContenderContainer_NoContender"]);
    }

    private Contender[] _generateContenders(in int size)
    {
        Contender[] arr = new Contender[size];
        for (int i = 0; i < size; i++)
        {
            arr[i] = new Contender($"Name{i}", $"Surname{i}", i);
        }

        return arr;
    }
}