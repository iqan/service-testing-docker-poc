using DummyService.App.Application.Handlers;
using DummyService.App.Application.Interfaces;
using DummyService.App.Infrastructure.Messaging;
using DummyService.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace DummyService.App
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configBuilder.AddJsonFile("appsettings.json", optional: true);
                    configBuilder.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configBuilder.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    configLogging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    configLogging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IEndpointConfiguration>(serviceProvider =>
                    {
                        return hostContext.Configuration.GetSection("EndpointConfiguration").Get<EndpointConfiguration>();
                    });

                    services.AddDbContext<DummyDbContext>(options => 
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DummyDatabase"));
                    });

                    services.AddScoped<IDatabaseService, DatabaseService>();

                    services.AddScoped<IDummyEventHandler, DummyEventHandler>();

                    services.AddScoped<IMessageReceiver, MessageReceiver>();

                    services.AddScoped<IHostedService, TimedHostedService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
