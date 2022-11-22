using HallWeb.ContenderContainer;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.Friend;
using HallWeb.Hall;
using HallWeb.utils;
using HallWeb.utils.ContenderNamesLoader;
using HallWeb.utils.ResultSaver;
using HallWeb.utils.WorldGeneratorClasses;
using Microsoft.EntityFrameworkCore;
using PrincessProject.Data.context;

var builder = WebApplication.CreateBuilder(args);
AddServices(builder, args);
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
    await worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
}

app.MapControllers();
app.Run();

void AddServices(WebApplicationBuilder appBuilder, string[] args)
{
    appBuilder.Services.AddDbContext<AttemptContext>(o =>
        o.UseNpgsql(appBuilder.Configuration.GetConnectionString("AttemptsDatabase")));
    var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
        .WithSeparator(';')
        .WithColumns(new string[1] { Constants.CsvNamesColumn });
    var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
        .WithSeparator(';')
        .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
    appBuilder.Services.AddSingleton<IContenderContainer, ContenderContainer>();
    appBuilder.Services.AddScoped<IResultSaver, VoidResultSaver>();
    appBuilder.Services.AddScoped((_) => new ContenderGenerator(namesLoader, surnamesLoader));
    appBuilder.Services.AddScoped<FromDatabaseContenderGenerator>();
    appBuilder.Services.AddScoped<IWorldGenerator, WorldGenerator>();
    appBuilder.Services.AddScoped<IFriend, Friend>();
    appBuilder.Services.AddScoped<IHall, Hall>();
}