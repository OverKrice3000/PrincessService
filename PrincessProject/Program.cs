using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject;
using PrincessProject.ContenderContainer;
using PrincessProject.ContenderGenerator;
using PrincessProject.Data.context;
using PrincessProject.Friend;
using PrincessProject.Hall;
using PrincessProject.PrincessClasses;
using PrincessProject.utils;
using PrincessProject.utils.AttemptLoader;
using PrincessProject.utils.ContenderNamesLoader;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                //services.AddDbContext<AttemptContext>(o => o.UseNpgsql(hostContext.Configuration.GetConnectionString("AttemptsDatabase")));
                services.AddDbContext<AttemptContext>(o =>
                    o.UseNpgsql(
                        "Server=localhost;Database=princess_database;Port=5432;User Id=postgres;Password=361993"));
                var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvNamesColumn });
                var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
                services.AddSingleton<IAttemptSaver, VoidAttemptSaver>();
                services.AddSingleton<IContenderGenerator, FromDatabaseContenderGenerator>((s) =>
                    new FromDatabaseContenderGenerator(s.GetRequiredService<AttemptContext>(), 43));
                services.AddSingleton<IContenderContainer, ContenderContainer>();
                services.AddSingleton<IFriend, Friend>();
                services.AddSingleton<IHall, Hall>();
                services.AddSingleton<IPrincess, Princess>();
                services.AddHostedService<PrincessService>();
            });
    }
}