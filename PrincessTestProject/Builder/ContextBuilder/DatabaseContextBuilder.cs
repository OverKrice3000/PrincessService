using Microsoft.EntityFrameworkCore;
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
            .Options;
        return new AttemptContext(options);
    }

    public DatabaseContextBuilder WithDatabaseName(string name)
    {
        _databaseName = name;
        return this;
    }
}