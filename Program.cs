using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SkunkCalc.Data;
using Microsoft.EntityFrameworkCore;

namespace SkunkCalc
{
    class Program
    {

        static async Task Main(string[] args)
        {
            //Setup Environment to allow us to switch settings files easily
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");

            //setup our DI
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //If you need a logger in the Main get it like this
            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("Starting application");

            var app = serviceProvider.GetService<Application>();
            await app.Run();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            logger.LogDebug("All done!");
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            IConfiguration configuration = GetConfiguration();
            services.AddSingleton<IConfiguration>(configuration);

            //bind appsettings.json to strongly typed class
            var config = new AppSettings();
            configuration.Bind(nameof(AppSettings), config);
            services.AddSingleton(config);

            //we will configure logging here
            services.AddLogging(
                config =>
                {
                    config.AddConsole();
                    config.AddDebug();
                })
            .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace)
            .AddTransient<Application>();

            //add db context
            services.AddDbContext<CalculatorContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("BloggingDatabase")));

            //register any other services for DI
            // .AddSingleton<IFooService, FooService>()
            // .AddSingleton<IBarService, BarService>()

        }

        private static IConfiguration GetConfiguration()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", true, true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .Build();
        }

    }

    public class AppSettings
    {
        public string ApplicationName { get; set; }
        public bool IsDemo { get; set; } = false;
    }

    public class Application
    {
        ILogger _logger;
        AppSettings _appsettings;

        public Application(ILogger<Application> logger, AppSettings config)
        {
            _logger = logger;
            _appsettings = config;
        }

        public async Task Run()
        {
            try
            {
                await Task.Run(() =>
                {
                    _logger.LogInformation($"This is a console application for MyApp {_appsettings.ApplicationName} 1");

                    if (_appsettings.IsDemo)
                    {
                        _logger.LogTrace($"================================DEMO===================================");
                        _logger.LogDebug($"================================DEMO===================================");
                        _logger.LogWarning($"================================DEMO===================================");
                        _logger.LogInformation($"================================DEMO===================================");
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

        }
    }

}
