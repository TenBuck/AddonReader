using System;
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

        private readonly Player _player = new Player();

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
                    await Task.Delay(100);


                    while (sink.Events.TryDequeue(out var logEvent))
                        ConsoleStrings.Add(DateTime.Now.ToLongTimeString() + ": " + logEvent);
                }
            });
        }


        public ObservableCollection<string> ConsoleStrings { get; set; }

      


        private async void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
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