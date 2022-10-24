using FluentAssertions;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.model;
using PrincessProject.utils.AttemptLoader;
using PrincessTestProject.Builder;
using PrincessTestProject.utils;

namespace PrincessTestProject.Tests.DatabaseTests;

public class DatabaseAttemptSaverLoaderTests
{
    private IContenderGenerator _attemptLoader;
    private DatabaseAttemptSaver _attemptSaver;
    private AttemptContext _context;
    private ContenderGenerator _generator;

    [OneTimeSetUp]
    public void SetUp()
    {
        _generator = (ContenderGenerator)TestBuilder
            .BuildIContenderGenerator()
            .BuildContenderGenerator()
            .Build();
        _context = TestBuilder
            .BuildDatabaseContext()
            .Build();
        _attemptSaver = TestBuilder
            .BuildIAttemptSaver()
            .BuildDatabaseAttemptSaver()
            .WithAttemptsContext(_context)
            .Build();
        _attemptLoader = TestBuilder
            .BuildIContenderGenerator()
            .BuildFromDatabaseContenderGenerator()
            .WithAttemptsContext(_context)
            .WithAttemptId(0)
            .Build();
        var worldGenerator = TestBuilder
            .BuildIWorldGenerator()
            .BuildWorldGenerator()
            .WithContext(_context)
            .WithAttemptSaver(_attemptSaver)
            .WithContendersGenerator(_generator)
            .Build();

        worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Attempts.RemoveRange(_context.Attempts.Where(a => a.AttemptId >= 100).ToArray());
        _context.SaveChanges();
    }

    [OneTimeTearDown]
    public void TotalCleanup()
    {
        _context.Attempts.RemoveRange(_context.Attempts.ToArray());
        _context.SaveChanges();
    }

    [Test]
    public void WorldShouldBeGeneratedCorrectly()
    {
        _context.Attempts.Count().Should().Be(Constants.DatabaseAttemptsGenerated *
                                              PrincessProject.utils.Constants.DefaultContendersCount);
    }

    [Test]
    public void ExistentAttemptCanBeLoadedFromDatabase()
    {
        var attempt = _context.Attempts.Where(a => a.AttemptId == 0).ToList()
            .MinBy(a => a.CandidateOrder);
        var contenders = _attemptLoader.Generate();
        new Contender(attempt.CandidateName, attempt.CandidateSurname, attempt.CandidateValue)
            .FullName
            .Should()
            .Be(contenders[0].FullName);
    }

    [Test]
    public void AttemptCanBeSavedToDatabase()
    {
        var nextAttemptId = _context.FindLastAttemptId() + 1;
        var contenders = _generator.Generate()
            .Select(c => new ContenderData(c.Name, c.Surname, c.Value)).ToArray();
        ;
        _attemptSaver.Save(new Attempt(contenders.Length, contenders, null));
        _context.Attempts
            .Count(a => a.AttemptId == nextAttemptId).Should().Be(contenders.Length);
        var attempt = _context.Attempts.Where(a => a.AttemptId == nextAttemptId).ToList()
            .MinBy(a => a.CandidateOrder);
        new Contender(attempt.CandidateName, attempt.CandidateSurname, attempt.CandidateValue)
            .FullName
            .Should()
            .Be(contenders[0].FullName);
    }
}