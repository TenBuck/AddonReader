using System.Windows;
using GregsStack.InputSimulatorStandard;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Events;
using TenBot;
using TenBot.AddonReader;
using TenBot.AddonReader.Boxes;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.ActionBars;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.SavedVariables;
using TenBot.Extensions.Services;
using ActionsReader = TenBot.AddonReader.Readers.ActionBars.ActionsReader;

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

            // WoW Related

            //saved variables

            services.AddSingleton<InMemoryActionBars>();
            services.AddSingleton<InMemoryKeyBinds>();

            services.AddSingleton<BotController>();
            services.AddSingleton<WowWindow>();
           
            services.AddSingleton<AddonConfigProvider>();

            services.AddSingleton<KeyBindSender>();

            
            services.AddSingleton<SavedVariablesParser>(s => new SavedVariablesParser("Licella", "Netherwind"));
            services.AddSingleton<BitmapProvider>();
            services.AddSingleton<BoxMgr>();
            services.AddSingleton<BoxBuilder>();


            services.AddReaders();

          

            // Input Related
            services.AddSingleton<IMouseSimulator,MouseSimulator>();
            services.AddSingleton<IKeyboardSimulator, KeyboardSimulator>();
        }
    }
}