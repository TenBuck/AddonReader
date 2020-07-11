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
            _logger.Information("Starting bot...");

            if (_wowWindow.Handle == IntPtr.Zero)
            {
                _logger.Error("WoW not found...");
                return;
            }
           

            try
            {
                _addonReaderMgr.Initialize();

                await Run(ct);
            }
            catch (OperationCanceledException)
            {
                _logger.Information("Bot Stopped");
              
            }
            catch (Exception e)
            {
                _logger.Error(e.Message );
            }
        }

        public async Task Run(CancellationToken ct)
        {

            
            while (true)
            {
               // _wowWindow.SetForeground();
               // _wowWindow.MoveWindow(Point.Empty);

                var player = new PlayerReader(_addonReaderMgr.BoxMgr);
                var target = new TargetReader(_addonReaderMgr.BoxMgr, "target");
                var targettarget = new TargetReader(_addonReaderMgr.BoxMgr, "targettarget");

                Log.Logger.Information("{@Player}", player);
                Log.Logger.Debug("{@Target}", target);
                Log.Logger.Debug("{@TargetTarget}", targettarget);

                await Task.Delay(250, ct);
               

                if (ct.IsCancellationRequested)
                {
                    _logger.Information("cancelling task:");
                    ct.ThrowIfCancellationRequested();
                }

                
            }
        }
    }
}