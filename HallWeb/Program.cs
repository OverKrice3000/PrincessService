using System.Text.Json.Nodes;
using HallWeb.ContenderContainer;
using HallWeb.ContenderGeneratorClasses;
using HallWeb.Friend;
using HallWeb.Hall;
using HallWeb.utils;
using HallWeb.utils.ContenderNamesLoader;
using HallWeb.utils.ResultSaver;
using HallWeb.utils.WorldGeneratorClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrincessProject.Data.context;
using PrincessProject.Data.model;
using PrincessProject.Data.model.api;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder, args);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var worldGenerator = scope.ServiceProvider.GetRequiredService<IWorldGenerator>();
    await worldGenerator.GenerateWorld(Constants.DatabaseAttemptsGenerated);
}

app.MapPost("/hall/{attemptId}/reset", (IHall hall, int attemptId) => { hall.Reset(attemptId); });

app.MapPost("/hall/{attemptId}/next", (IHall hall, int attemptId) =>
{
    try
    {
        VisitingContender contender = hall.GetNextContender(attemptId);
        return Results.Ok(new JsonObject()
        {
            ["name"] = contender.FullName
        });
    }
    catch (ArgumentException e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapPost("/hall/{attemptId}/select", (IHall hall, int attemptId) =>
{
    try
    {
        int rank = hall.ChooseContender(attemptId);
        return Results.Ok(new JsonObject()
        {
            ["rank"] = rank
        });
    }
    catch (ArgumentException e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.MapPost("/friend/{attemptId}/compare", (IFriend friend, int attemptId, [FromBody] CompareApiPayload json) =>
{
    try
    {
        VisitingContender firstContender = Util.VisitingContenderFromFullName(json.first);
        VisitingContender secondContender = Util.VisitingContenderFromFullName(json.second);
        return Results.Ok(new JsonObject()
        {
            ["name"] = friend.CompareContenders(attemptId, firstContender, secondContender).FullName
        });
    }
    catch (ArgumentException e)
    {
        return Results.BadRequest(e.Message);
    }
});

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