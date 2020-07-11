using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TenBot;

namespace TenBotApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BotController _botController;

        private readonly object consoleStringsLock = new object();
        private InMemorySink _sink;
        private CancellationTokenSource cts;

        public MainWindow(BotController botController, InMemorySink sink)
        {
            InitializeComponent();
            _botController = botController;
            _sink = sink;
            DataContext = this;

            ConsoleStrings = new ObservableCollection<string>();
            BindingOperations.EnableCollectionSynchronization(ConsoleStrings, consoleStringsLock);


            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(50);
                    var logEvent = "";
                    while (sink.Events.TryDequeue(out logEvent)) ConsoleStrings.Add(logEvent);
                }
            });
        }

        public ObservableCollection<string> ConsoleStrings { get; set; }


        private async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            await _botController.Start(cts.Token);
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }
    }
}