using System;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

using TenBot;
using TenBot.AddonReader;
using TenBot.AddonReader.Frames;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider ServiceProvider;

            ServiceCollection services = new ServiceCollection();


            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();


            var botController = ServiceProvider.GetService<BotController>();
            botController.Start();



        }

        private static void ConfigureServices(IServiceCollection services)
        {


           // Log.Logger = new LoggerConfiguration().CreateLogger();
            // Configuration Builder

           // services.AddSingleton<ILogger>(Log.Logger);


            // WoW Related
            services.AddTransient<BotController>();
            services.AddTransient<WowWindow>();
            services.AddSingleton<BitmapProvider>();
            services.AddSingleton<AddonReaderMgr>();
            
        }
    }
}

