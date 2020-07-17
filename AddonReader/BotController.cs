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
        private readonly WowWindow _wowWindow;


        public BotController(AddonReaderMgr addonReaderMgr, WowWindow wowWindow,
            KeyBindSender keyBindSender)
        {
            _addonReaderMgr = addonReaderMgr;
            _wowWindow = wowWindow;

            _keyBindSender = keyBindSender;
        }

        public bool IsRunning { get; set; }

        public async Task Start(CancellationToken ct)
        {
            Log.Logger.Information("Starting bot...");

            if (_wowWindow.Handle == IntPtr.Zero)
            {
                Log.Logger.Error("WoW not found...");
                return;
            }

            _wowWindow.MoveWindow(Point.Empty);
            _addonReaderMgr.Dump();
            _botStates.Push(new AcquireTargetState(_botStates, _addonReaderMgr, _keyBindSender));

            try
            {
                IsRunning = true;
                await Run(ct);
            }
            catch (OperationCanceledException)
            {
                IsRunning = false;
                Log.Logger.Information("Bot Stopped");
            }
            catch (Exception e)
            {
                Log.Logger.Error(e.Message);
            }
        }

        private async Task Run(CancellationToken ct)
        {
            while (IsRunning)
            {
                if (ct.IsCancellationRequested)
                {
                    Log.Logger.Information("cancelling task:");
                    ct.ThrowIfCancellationRequested();
                }

                await Task.Delay(300);
                _addonReaderMgr.Dump();
                await _botStates.Peek()?.Update()!;
            }
        }
    }
}