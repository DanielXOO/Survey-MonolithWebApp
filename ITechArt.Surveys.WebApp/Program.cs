using System;
using System.Threading.Tasks;
using iTechArt.Common.Microsoft.Logging;
using iTechArt.Common.Time;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ILogger = iTechArt.Common.Logging.ILogger;

namespace iTechArt.Surveys.WebApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await CreateDatabaseIfNotExists(host);
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging((ctx, builder) =>
                {
                    builder.AddLogger();
                    builder.AddFile(ctx.Configuration.GetSection("Serilog:FileLogging"));
                });
        }


        private static async Task CreateDatabaseIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger>();
                try
                {
                    var context = services.GetRequiredService<SurveysDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();
                    var systemClock = services.GetService<ISystemClock>();
                    await DbInitializer.Initialize(context, userManager, roleManager, systemClock);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error DB initialization");
                    throw;
                }
            }
        }
    }
}