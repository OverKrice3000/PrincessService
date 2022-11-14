﻿using HallWeb.ContenderGeneratorClasses;
using HallWeb.utils.AttemptSaver;
using PrincessProject.Data.context;
using PrincessProject.Data.model;

namespace HallWeb.utils.WorldGeneratorClasses;

public class WorldGenerator : IWorldGenerator
{
    private IAttemptSaver _attemptSaver;
    private AttemptContext _context;
    private IContenderGenerator _generator;

    public WorldGenerator(
        ContenderGenerator generator,
        DatabaseAttemptSaver attemptSaver,
        AttemptContext context
    )
    {
        _generator = generator;
        _attemptSaver = attemptSaver;
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
            await _attemptSaver.Save(new Attempt(contenders.Length, contenders, null));
        }
    }
}