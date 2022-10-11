using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.WorldGeneratorClasses;

namespace PrincessTestProject.Builder.WorldGeneratorBuilder;

public class WorldGeneratorBuilder
{
    private IAttemptSaver _attemptSaver;
    private AttemptContext _context;
    private IContenderGenerator _generator;

    public WorldGeneratorBuilder()
    {
        _context = TestBuilder.BuildDatabaseContext()
            .Build();
        _generator = TestBuilder.BuildIContenderGenerator()
            .BuildContenderGenerator()
            .Build();
        _attemptSaver = TestBuilder
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

    public WorldGeneratorBuilder WithAttemptSaver(IAttemptSaver attemptSaver)
    {
        _attemptSaver = attemptSaver;
        return this;
    }

    public WorldGeneratorBuilder WithContendersGenerator(IContenderGenerator generator)
    {
        _generator = generator;
        return this;
    }

    public IWorldGenerator Build()
    {
        return new WorldGenerator(_generator, _attemptSaver, _context);
    }
}