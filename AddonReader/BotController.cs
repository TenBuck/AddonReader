using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GregsStack.InputSimulatorStandard.Native;
using PInvoke;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;
using TenBot.Game.WowTypes;

namespace TenBot
{
    public class BotController
    {
        private readonly AddonReaderMgr _addonReaderMgr;


        private readonly WowWindow _wowWindow;
        private readonly ILogger _logger;
        private readonly KeyBindSender _keyBindSender;

        public BotController(AddonReaderMgr addonReaderMgr, WowWindow wowWindow, ILogger logger, KeyBindSender keyBindSender)
        {
            _addonReaderMgr = addonReaderMgr;
            _wowWindow = wowWindow;
            _logger = logger;
            _keyBindSender = keyBindSender;
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

        private async Task Run(CancellationToken ct)
        {

            
            while (true)
            {
                var player = new PlayerReader(_addonReaderMgr.BoxMgr);
                var target = new TargetReader(_addonReaderMgr.BoxMgr, "target");
                var targettarget = new TargetReader(_addonReaderMgr.BoxMgr, "targettarget");

                Log.Logger.Information("{@Player}", player);
                Log.Logger.Debug("{@Target}", target);
                Log.Logger.Debug("{@TargetTarget}", targettarget);

                await Task.Delay(200);
                if (ct.IsCancellationRequested)
                {
                    _logger.Information("cancelling task:");
                    ct.ThrowIfCancellationRequested();
                }

                
            }
        }
    }
}