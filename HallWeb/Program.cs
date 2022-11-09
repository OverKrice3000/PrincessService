using HallWeb.ContenderContainer;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.Friend;
using HallWeb.Hall;
using HallWeb.utils;
using HallWeb.utils.AttemptSaver;
using HallWeb.utils.ContenderNamesLoader;
using HallWeb.utils.WorldGeneratorClasses;
using Microsoft.EntityFrameworkCore;
using PrincessProject.Data.context;

class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder, args);

        var app = builder.Build();

        app.Run();
    }

    private static void AddServices(WebApplicationBuilder builder, string[] args)
    {
        builder.Services.AddDbContext<AttemptContext>(o =>
            o.UseNpgsql(builder.Configuration.GetConnectionString("AttemptsDatabase")));
        var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
            .WithSeparator(';')
            .WithColumns(new string[1] { Constants.CsvNamesColumn });
        var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
            .WithSeparator(';')
            .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
        builder.Services.AddScoped<DatabaseAttemptSaver>();
        builder.Services.AddScoped<IAttemptSaver, VoidAttemptSaver>();
        builder.Services.AddScoped((_) => new ContenderGenerator(namesLoader, surnamesLoader));
        builder.Services.AddScoped<IContenderGenerator, FromDatabaseContenderGenerator>();
        builder.Services.AddScoped<IContenderContainer, ContenderContainer>();
        builder.Services.AddScoped<IWorldGenerator, WorldGenerator>();
        builder.Services.AddScoped<IFriend, Friend>();
        builder.Services.AddSingleton<IHall, Hall>();
    }

    /*public static void GenerateWorldFromPrincessProject()
    {
        using var scope = _scopeFactory.CreateScope();
        var generator =
            (FromDatabaseContenderGenerator)scope.ServiceProvider.GetRequiredService<IContenderGenerator>();
        var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
        //TODO move world generator to hall web application
        worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
    }*/
}