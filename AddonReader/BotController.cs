using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.Core;
using TenBot.Core.States.Combat;

namespace TenBot
{
    public class BotController
    {
        private readonly AddonReaderMgr _addonReaderMgr;

        private readonly Stack<IBotState> _botStates = new Stack<IBotState>();
        private readonly KeyBindSender _keyBindSender;
        private readonly ILogger _logger;
        private readonly WowWindow _wowWindow;
      

        public BotController(AddonReaderMgr addonReaderMgr, WowWindow wowWindow, ILogger logger,
            KeyBindSender keyBindSender)
        {
            _addonReaderMgr = addonReaderMgr;
            _wowWindow = wowWindow;
            _logger = logger;
            _keyBindSender = keyBindSender;
        }

        public bool IsRunning { get; set; }

        public async Task Start(CancellationToken ct)
        {
            _logger.Information("Starting bot...");

            if (WowWindow.Handle == IntPtr.Zero)
            {
                _logger.Error("WoW not found...");
                return;
            }

            _wowWindow.MoveWindow(Point.Empty);
            _addonReaderMgr.Dump();
            _botStates.Push(new AcquireTargetState(_logger, _botStates, _addonReaderMgr, _keyBindSender));

            try
            {
                IsRunning = true;
                await Run(ct);
            }
            catch (OperationCanceledException)
            {
                _logger.Information("Bot Stopped");
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
            }
        }

        private async Task Run(CancellationToken ct)
        {
            while (IsRunning)
            {
                await Task.Delay(500);
                if (ct.IsCancellationRequested)
                {
                    _logger.Information("cancelling task:");
                    ct.ThrowIfCancellationRequested();
                }

                await _botStates.Peek()?.Update()!;

            }
        }
    }
}