using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.utils.AttemptLoader;
using PrincessTestProject.Builder;

namespace PrincessTestProject.Tests.DatabaseTests;

public class DatabaseAttemptSaverLoaderTests
{
    private IContenderGenerator _attemptLoader;
    private IAttemptSaver _attemptSaver;
    private AttemptContext _context;

    [OneTimeSetUp]
    public void SetUp()
    {
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
            .Build();
    }

    [Test]
    public void AttemptShouldBeInDatabaseAfterSavedToDatabase()
    {
    }

    [Test]
    public void ExistentAttemptCanBeLoadedFromDatabase()
    {
    }

    [Test]
    public void AttemptCanBeLoadedAfterBeingSavedToDatabase()
    {
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Attempts.RemoveRange(_context.Attempts.ToArray());
        _context.SaveChanges();
    }
}