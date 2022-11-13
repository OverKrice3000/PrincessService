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
    public async Task SetUp()
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

        await worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
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

    /// <summary>
    /// Mock generator returns equal attempts
    /// Therefore we load attempt from database and compare it to newly generated attempt
    /// </summary>
    [Test]
    public void ExistentAttemptCanBeLoadedFromDatabase()
    {
        var contendersDb = _context.Attempts.Where(a => a.AttemptId == 0).OrderBy(a => a.CandidateOrder).ToList();
        var contendersLoader = _attemptLoader.Generate();
        for (int i = 0; i < contendersLoader.Length; i++)
        {
            var contenderDb =
                new Contender(contendersDb[i].CandidateName, contendersDb[i].CandidateSurname,
                    contendersDb[i].CandidateValue);
            contenderDb.Should().BeEquivalentTo(contendersLoader[i]);
        }
    }

    [Test]
    public async Task AttemptCanBeSavedToDatabase()
    {
        var nextAttemptId = _context.Attempts.Any() ? _context.FindLastAttemptId() + 1 : 0;
        var contenders = _generator.Generate()
            .Select(c => new ContenderData(c.Name, c.Surname, c.Value)).ToArray();
        await _attemptSaver.Save(new Attempt(contenders.Length, contenders, null));
        _context.Attempts
            .Count(a => a.AttemptId == nextAttemptId).Should().Be(contenders.Length);
        var attempt = _context.Attempts.Where(a => a.AttemptId == nextAttemptId).ToList()
            .MinBy(a => a.CandidateOrder);
        attempt.Should().NotBeNull();
        new Contender(attempt!.CandidateName, attempt.CandidateSurname, attempt.CandidateValue)
            .FullName
            .Should()
            .Be(contenders[0].FullName);
    }
}