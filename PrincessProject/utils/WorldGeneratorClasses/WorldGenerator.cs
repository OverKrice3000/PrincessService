using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.model;
using PrincessProject.utils.AttemptLoader;

namespace PrincessProject.utils.WorldGeneratorClasses;

public class WorldGenerator : IWorldGenerator
{
    private IAttemptSaver _attemptSaver;
    private AttemptContext _context;
    private IContenderGenerator _generator;

    public WorldGenerator(
        IContenderGenerator generator,
        IAttemptSaver attemptSaver,
        AttemptContext context
    )
    {
        _generator = generator;
        _attemptSaver = attemptSaver;
        _context = context;
    }

    public void GenerateWorld(int attempts)
    {
        int k = 0;
        if (_context.Attempts.Any())
        {
            return;
        }

        for (int i = 0; i < Constants.DatabaseAttemptsGenerated; i++)
        {
            var contenders = _generator.Generate(Constants.DefaultContendersCount)
                .Select(c => new ContenderData(c.Name, c.Surname, c.Value)).ToArray();
            _attemptSaver.Save(new Attempt(contenders.Length, contenders, null));
        }

        _context.SaveChanges();
    }
}