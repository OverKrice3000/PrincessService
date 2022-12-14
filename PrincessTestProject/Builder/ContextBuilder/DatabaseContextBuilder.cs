using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PrincessProject.Data.context;
using PrincessTestProject.utils;

namespace PrincessTestProject.Builder.ContextBuilder;

public class DatabaseContextBuilder
{
    private string _databaseName = Constants.InMemoryDatabaseDefaultName;

    public AttemptContext Build()
    {
        var options = new DbContextOptionsBuilder<AttemptContext>()
            .UseInMemoryDatabase(databaseName: _databaseName)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        return new AttemptContext(options);
    }
}