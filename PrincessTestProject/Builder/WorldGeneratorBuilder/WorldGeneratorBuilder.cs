using HallWeb.ContenderGeneratorClasses;
using HallWeb.utils.AttemptSaver;
using HallWeb.utils.WorldGeneratorClasses;
using PrincessProject.Data.context;

namespace PrincessTestProject.Builder.WorldGeneratorBuilder;

public class WorldGeneratorBuilder
{
    private DatabaseAttemptSaver _attemptSaver;
    private AttemptContext _context;
    private ContenderGenerator _generator;

    public WorldGeneratorBuilder()
    {
        _context = TestBuilder.BuildDatabaseContext()
            .Build();
        _generator = (ContenderGenerator)TestBuilder.BuildIContenderGenerator()
            .BuildContenderGenerator()
            .Build();
        _attemptSaver = (DatabaseAttemptSaver)TestBuilder
            .BuildIAttemptSaver()
            .BuildDatabaseAttemptSaver()
            .WithAttemptsContext(_context)
            .Build();
    }

    public WorldGeneratorBuilder WithContext(AttemptContext context)
    {
        _context = context;
        return this;
    }

    public WorldGeneratorBuilder WithAttemptSaver(DatabaseAttemptSaver attemptSaver)
    {
        _attemptSaver = attemptSaver;
        return this;
    }

    public WorldGeneratorBuilder WithContendersGenerator(ContenderGenerator generator)
    {
        _generator = generator;
        return this;
    }

    public IWorldGenerator Build()
    {
        return new WorldGenerator(_generator, _attemptSaver, _context);
    }
}