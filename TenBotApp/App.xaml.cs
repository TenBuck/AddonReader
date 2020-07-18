using System.Windows;
using GregsStack.InputSimulatorStandard;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using TenBot;
using TenBot.AddonReader;
using TenBot.AddonReader.SavedVariables;
using TenBot.Extensions.Services;

namespace TenBotApp
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();


            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetService<MainWindow>();
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

            services.AddSingleton(Log.Logger);
            services.AddSingleton(sink);


            // UI
            services.AddSingleton<MainWindow>();


            // Input Related
            services.AddSingleton<IMouseSimulator, MouseSimulator>();
            services.AddSingleton<IKeyboardSimulator, KeyboardSimulator>();

            // WoW Related
            services.AddSingleton<InMemoryActionBars>();
            services.AddSingleton<InMemoryKeyBinds>();

            services.AddSingleton<BotController>();
            services.AddSingleton<WowWindow>();


            services.AddSingleton<KeyBindSender>();


            services.AddSingleton<BitmapProvider>();


            services.AddReaders();
            services.AddPlayer();


          
        }
    }
}