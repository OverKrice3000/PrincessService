using FluentAssertions;
using PrincessProject.Data.context;
using PrincessProject.PrincessClasses;
using PrincessTestProject.Builder;

namespace PrincessTestProject.Tests.DatabaseTests;

public class PrincessSimulationTests
{
    private const int ContendersInContainerCount = 100;
    private AttemptContext _context;
    private IPrincess _princess;

    [OneTimeSetUp]
    public void SetUp()
    {
        _context = TestBuilder
            .BuildDatabaseContext()
            .Build();
        var attemptSaver = TestBuilder
            .BuildIAttemptSaver()
            .BuildDatabaseAttemptSaver()
            .WithAttemptsContext(_context)
            .Build();
        var worldGenerator = TestBuilder
            .BuildIWorldGenerator()
            .BuildWorldGenerator()
            .WithContext(_context)
            .WithAttemptSaver(attemptSaver)
            .Build();

        worldGenerator.GenerateWorld(1);

        var attemptLoader = TestBuilder
            .BuildIContenderGenerator()
            .BuildFromDatabaseContenderGenerator()
            .WithAttemptsContext(_context)
            .WithAttemptId(0)
            .Build();

        var container = TestBuilder
            .BuildIContenderContainer()
            .BuildContenderContainer()
            .WithContenderGenerator(attemptLoader)
            .Build();

        var friend = TestBuilder
            .BuildIFriend()
            .BuildFriend()
            .WithContainer(container)
            .Build();
        var hall = TestBuilder
            .BuildIHall()
            .BuildHall()
            .WithContainer(container)
            .WithFriend(friend)
            .WithSize(ContendersInContainerCount)
            .Build();

        _princess = TestBuilder
            .BuildIPrincess()
            .BuildPrincess()
            .WithHall(hall)
            .Build();
    }

    [OneTimeTearDown]
    public void TotalCleanup()
    {
        _context.Attempts.RemoveRange(_context.Attempts.ToArray());
        _context.SaveChanges();
    }

    /// <summary>
    /// Algorithm for choosing husband should return consistent results
    /// </summary>
    [Test]
    public void ResultsOfChoosingHusbandShouldBeConsistent()
    {
        _princess.ChooseHusband().Should().Be(_princess.ChooseHusband());
    }
}