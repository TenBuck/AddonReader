using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Boxes;
using TenBot.Core;
using TenBot.Core.States.Combat;
using TenBot.Core.WowPlayer;

namespace TenBot
{
    public class BotController
    {
        private readonly Stack<IBotState> _botStates = new Stack<IBotState>();
        private readonly KeyBindSender _keyBindSender;
        private readonly BoxMgr _boxMgr;
        private readonly Player _player;
        private readonly WowWindow _wowWindow;


        public BotController(Player player, WowWindow wowWindow,
            KeyBindSender keyBindSender, BoxMgr boxMgr)
        {
            _player = player;
            _wowWindow = wowWindow;
            _keyBindSender = keyBindSender;
            _boxMgr = boxMgr;
        }

        public bool IsRunning { get; set; }

        public async Task Start(CancellationToken ct)
        {
            _boxMgr.Refresh();
            Log.Logger.Information("Starting bot...");

            if (_wowWindow.Handle == IntPtr.Zero)
            {
                Log.Logger.Error("WoW not found...");
                return;
            }

            try
            {
                _boxMgr.IsAddonVisible();
            }
            catch (Exception e)
            {
                Log.Logger.Error("Addon not visible");
                return;
            }
            _wowWindow.MoveWindow(Point.Empty);

            await _player.Rotate(50);
            _botStates.Push(new AcquireTargetState(_botStates, _player, _keyBindSender));

            _player.Dump();
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

                await Task.Delay(400);
                await _botStates.Peek()?.Update()!;
            }
        }
    }
}