﻿using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrincessProject;
using PrincessProject.consumers;
using PrincessProject.PrincessClasses;

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
                services.AddMassTransit(config =>
                {
                    config.AddConsumer<ContenderConsumer>();

                    config.UsingRabbitMq((ctx, cfg) =>
                    {
                        cfg.Host("amqp://guest:guest@localhost:5672");
                        cfg.ReceiveEndpoint("demo-queue", c => { c.ConfigureConsumer<ContenderConsumer>(ctx); });
                    });
                });

                services.AddScoped<IPrincess, Princess>();
                services.AddScoped<ContenderConsumer>();
                services.AddHostedService<PrincessService>();
            });
    }
}