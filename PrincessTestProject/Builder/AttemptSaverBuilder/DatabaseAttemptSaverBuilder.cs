using HallWeb.utils.AttemptSaver;
using PrincessProject.Data.context;

namespace PrincessTestProject.Builder.AttemptSaverBuilder;

public class DatabaseAttemptSaverBuilder
{
    private AttemptContext _context = TestBuilder.BuildDatabaseContext().Build();

    public DatabaseAttemptSaverBuilder WithAttemptsContext(AttemptContext context)
    {
        _context = context;
        return this;
    }

    public DatabaseAttemptSaver Build()
    {
        return new DatabaseAttemptSaver(_context);
    }
}