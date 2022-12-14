using FluentAssertions;
using HallWeb.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessTestProject.Builder;
using PrincessTestProject.utils;

namespace PrincessTestProject.Tests.DatabaseTests;

public class DatabaseAttemptSaverLoaderTests
{
    private IContenderGenerator _attemptLoader;
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
                                              PrincessProject.Data.Constants.DefaultContendersCount);
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
}