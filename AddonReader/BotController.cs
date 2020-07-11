using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;

namespace TenBot
{
    public class BotController
    {
        private readonly AddonReaderMgr _addonReaderMgr;


        private readonly WowWindow _wowWindow;
        private readonly ILogger _logger;

        public BotController(AddonReaderMgr addonReaderMgr, WowWindow wowWindow, ILogger logger)
        {
            _addonReaderMgr = addonReaderMgr;
            _wowWindow = wowWindow;
            _logger = logger;
        }

        public async Task Start(CancellationToken ct)
        {
            if (_wowWindow.Handle == IntPtr.Zero)
            {
                _logger.Error("WoW not found...");
                return;
            }
            _addonReaderMgr.Initialize();
            try
            {
                await Run(ct);
            }
            catch (OperationCanceledException)
            {
                _logger.Information("Bot Stopped");
              
            }
            catch (Exception)
            {

            }
        }

        public async Task Run(CancellationToken ct)
        {

            
            while (true)
            {
               // _wowWindow.SetForeground();
               // _wowWindow.MoveWindow(Point.Empty);

                var unit = new UnitReader(_addonReaderMgr.BoxMgr, "player");

                await Task.Delay(250, ct);
                Log.Logger.Information("{@unit}", unit);

                if (ct.IsCancellationRequested)
                {
                    _logger.Information("cancelling task:");
                    ct.ThrowIfCancellationRequested();
                }

                
            }
        }
    }
}