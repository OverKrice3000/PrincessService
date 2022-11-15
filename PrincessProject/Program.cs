using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject;
using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGeneratorClasses;
using PrincessProject.Data.context;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.ContenderNamesLoader;
using PrincessProject.utils.WorldGeneratorClasses;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration((builder => { builder.AddJsonFile("appsettings.json"); }))
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<AttemptContext>(o =>
                    o.UseNpgsql(hostContext.Configuration.GetConnectionString("AttemptsDatabase")));
                var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvNamesColumn });
                var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
                services.AddScoped<DatabaseAttemptSaver>();
                services.AddScoped<IAttemptSaver, VoidAttemptSaver>();
                services.AddScoped<ContenderGenerator>((s) =>
                    new ContenderGenerator(namesLoader, surnamesLoader));
                services.AddScoped<IContenderGenerator, FromDatabaseContenderGenerator>((s) =>
                    new FromDatabaseContenderGenerator(s.GetRequiredService<AttemptContext>(),
                        int.Parse(args.Length > 0 ? args[0] : "0")));
                services.AddScoped<IContenderContainer, ContenderContainer>();
                services.AddScoped<IWorldGenerator, WorldGenerator>();
                services.AddScoped<IFriend, Friend>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IPrincess, Princess>();
                services.AddHostedService<PrincessService>();
            });
    }
}