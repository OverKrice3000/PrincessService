﻿using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;

namespace PrincessTestProject.Builder.ContenderGeneratorBuilder;

public class FromDatabaseContenderGeneratorBuilder
{
    private int _attemptId = 0;
    private AttemptContext _context = TestBuilder.BuildDatabaseContext().Build();

    public FromDatabaseContenderGeneratorBuilder WithAttemptId(int attemptId)
    {
        _attemptId = attemptId;
        return this;
    }

    public FromDatabaseContenderGeneratorBuilder WithAttemptsContext(AttemptContext context)
    {
        _context = context;
        return this;
    }

    public FromDatabaseContenderGenerator Build()
    {
        return new FromDatabaseContenderGenerator(_context, _attemptId);
    }
}