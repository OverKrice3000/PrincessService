using HallWeb.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessProject.Data.model.data;

namespace HallWeb.utils.WorldGeneratorClasses;

public class WorldGenerator : IWorldGenerator
{
    private AttemptContext _context;
    private IContenderGenerator _generator;

    public WorldGenerator(
        ContenderGenerator generator,
        AttemptContext context
    )
    {
        _generator = generator;
        _context = context;
    }

    public async Task GenerateWorld(int attempts = Constants.DatabaseAttemptsGenerated)
    {
        if (_context.Attempts.Any())
        {
            return;
        }

        for (int i = 0; i < attempts; i++)
        {
            var contenders = _generator.Generate()
                .Select(c => new ContenderData(c.Name, c.Surname, c.Value)).ToArray();
            await _context.SaveAttempt(new Attempt(contenders.Length, contenders));
        }
    }
}