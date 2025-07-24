using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;
using SprinklerPlannerApp.Infrastructure.Data;
using SprinklerPlannerApp.Infrastructure.Output;
using SprinklerPlannerApp.Infrastructure.Services;
using SprinklerPlannerApp.Runner;
using System;
using System.Collections.Generic;
using System.IO;

namespace SprinklerPlannerApp.Config
{
    public static class ServiceRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}")
                .WriteTo.File("Logs/sprinklers.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });
            
            services.AddSingleton<IOutputPrinter, ConsoleOutputPrinter>();

            services.AddSingleton<IRoomDataSeeder, RoomDataSeeder>();
            services.AddSingleton<IPipeDataSeeder, PipeDataSeeder>();

            services.AddSingleton<Room>(provider =>
            {
                IRoomDataSeeder roomSeeder = provider.GetRequiredService<IRoomDataSeeder>();
                string basePath = AppContext.BaseDirectory;
                string roomCsvPath = Path.Combine(basePath, "Resources", "room.csv");
                return roomSeeder.LoadFromCsv(roomCsvPath);
            });

            services.AddSingleton<List<LineSegment>>(provider =>
            {
                IPipeDataSeeder pipeSeeder = provider.GetRequiredService<IPipeDataSeeder>();
                string basePath = AppContext.BaseDirectory;
                string pipeCsvPath = Path.Combine(basePath, "Resources", "pipe.csv");
                return pipeSeeder.LoadFromCsv(pipeCsvPath);
            });

            services.AddSingleton<ISprinklerPlannerService>(provider =>
            {
                Room room = provider.GetRequiredService<Room>();
                List<LineSegment> pipes = provider.GetRequiredService<List<LineSegment>>();
                ILogger<SprinklerPlannerService> logger = provider.GetRequiredService<ILogger<SprinklerPlannerService>>();

                return new SprinklerPlannerService(room, pipes, logger);
            });

            services.AddSingleton<AppRunner>();
        }
    }
}
