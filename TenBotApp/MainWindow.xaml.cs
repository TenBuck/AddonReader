using System;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
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

        private Player _player = new Player();


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
                    await Task.Delay(100);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        AllowTrailingCommas = true,
                        MaxDepth = 2,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                    };
                    var logEvent = "";
                    while (sink.Events.TryDequeue(out logEvent))
                    {
                        if (logEvent.Contains("$type\":\"PlayerReader"))
                        {
                            _player = JsonSerializer.Deserialize<Player>(logEvent, options);
                            await Dispatcher.BeginInvoke(UpdateUi);
                        }
                        else
                        {
                            ConsoleStrings.Add(DateTime.Now.ToLongTimeString() + ": "  + logEvent);
                        }
                   
                    }
                }
            });
        }


        public ObservableCollection<string> ConsoleStrings { get; set; }

        public void UpdateUi()
        {
            Health.Text = "Health: " + _player.Health;
            Level.Text = "Level: " + _player.Level;
            Name.Text = "Name: " + _player.Name;
            Durability.Text = "Durability: " + _player.Durability;
            Freeslots.Text = "Freeslots: " + _player.Freeslots;
        }


        private async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            PlayerPanel.Visibility = Visibility.Visible;
            startBtn.IsEnabled = false;
            await _botController.Start(cts.Token);
            startBtn.IsEnabled = true;
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
            PlayerPanel.Visibility = Visibility.Hidden;
        }
    }

    public class Player
    {
        public int Casting { get; set; }
        public int Durability { get; set; }
        public int Freeslots { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int HealthMax { get; set; }
        public int Power { get; set; }
        public int PowerMax { get; set; }
        public int Level { get; set; }
        public bool IsDead { get; set; }
        public int MovingSpeed { get; set; }
      
    }
}