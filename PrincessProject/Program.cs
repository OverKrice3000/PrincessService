using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject;
using PrincessProject.ContenderGenerator;
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
                var namesLoader = new CsvLoader(Constants.FromProjectRootCsvNamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvNamesColumn });
                var surnamesLoader = new CsvLoader(Constants.FromProjectRootCsvSurnamesFilepath)
                    .WithSeparator(';')
                    .WithColumns(new string[1] { Constants.CsvSurnamesColumn });
                services.AddSingleton<IAttemptSaver, FileAttemptSaver>();
                services.AddSingleton<IContenderGenerator, ContenderGenerator>((s) =>
                    new ContenderGenerator(namesLoader, surnamesLoader));
                services.AddSingleton<IFriend, Friend>();
                services.AddSingleton<IHall, Hall>();
                services.AddSingleton<IPrincess, Princess>();
                services.AddHostedService<PrincessService>();
            });
    }
}