using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using TenBot;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.SavedVariables;

namespace TenBotApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            
            var services = new ServiceCollection();


            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            var sink = new InMemorySink();
           

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Sink(sink, LogEventLevel.Information)
                .CreateLogger();

           
            // Configuration Builder

            services.AddSingleton<ILogger>(Log.Logger);
            
            services.AddSingleton<MainWindow>();

            // WoW Related
            services.AddSingleton(sink);
            services.AddTransient<BotController>();
            services.AddSingleton<WowWindow>();
            services.AddSingleton<PlayerReader>();
            
            services.AddSingleton(s => new SavedVariablesParser("Jetherenn", "Netherwind"));
            services.AddSingleton<BitmapProvider>();
            services.AddSingleton<AddonReaderMgr>();
        }
    }



  
}
