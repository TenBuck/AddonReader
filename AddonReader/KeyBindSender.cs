using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using Serilog;
using TenBot.AddonReader.SavedVariables;
using TenBot.Game.WowTypes;

namespace TenBot
{
    public class KeyBindSender
    {
        private readonly InMemoryKeyBinds _keyBinds;

        private readonly IKeyboardSimulator _simulator;
        private readonly WowWindow _wowWindow;
        private readonly int SleepTime = 100;

        public KeyBindSender(WowWindow wowWindow, IKeyboardSimulator simulator, InMemoryKeyBinds keyBinds,
            ILogger logger)
        {
            _wowWindow = wowWindow;
            _simulator = simulator;
            _keyBinds = keyBinds;
        }

        public async Task SimulateKeyPress(KeyBinding kb)
        {
            _wowWindow.SetForeground();

            await SimulateKeyPress(_keyBinds.GetKeyBind(kb));
        }

        private async Task SimulateKeyPress(IEnumerable<VirtualKeyCode> keys)
        {
            Log.Logger.Information("Pressing {@keys}", keys);
            _wowWindow.SetForeground();
            foreach (var key in keys) await SimulateKeyDown(key);

            await Sleep(SleepTime);

            foreach (var key in keys.Reverse()) await SimulateKeyUp(key);
            await Sleep(SleepTime);
        }


        private async Task SimulateKeyUp(VirtualKeyCode keyCode)
        {
            _simulator.KeyUp(keyCode);
        }

        private async Task SimulateHoldKey(VirtualKeyCode keyCode, CancellationToken ct)
        {
            _wowWindow.SetForeground();
            _simulator.KeyPress(keyCode);
            await Sleep(190);
            while (!ct.IsCancellationRequested)
            {
                _wowWindow.SetForeground();
                _simulator.KeyUp(keyCode);
                await Sleep(100);
            }
        }

        private async Task SimulateKeyDown(VirtualKeyCode keyCode)
        {
            _simulator.KeyDown(keyCode);
        }


        private async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}