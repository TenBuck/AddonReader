using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TenBot;
using TenBot.AddonReader;
using TenBot.AddonReader.SavedVariables;

namespace ConsoleApp1
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IServiceProvider ServiceProvider;

            var services = new ServiceCollection();


            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();


            var botController = ServiceProvider.GetService<BotController>();
            await botController.Start();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            // Configuration Builder

            services.AddSingleton(Log.Logger);


            // WoW Related
            services.AddSingleton<BotController>();
            services.AddSingleton<WowWindow>();
            services.AddSingleton(new SavedVariablesParser("Jetherenn", "Netherwind"));
            services.AddSingleton<BitmapProvider>();
            services.AddSingleton<AddonReaderMgr>();
        }
    }
}