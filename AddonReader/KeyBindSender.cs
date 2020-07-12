using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using Serilog;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot
{
    public class KeyBindSender
    {
        private readonly Dictionary<KeyBinding, KeyBind> _keyBindingDictionary;
        private readonly ILogger _logger;

        private readonly IKeyboardSimulator _simulator;
        private readonly WowWindow _wowWindow;
        private readonly int SleepTime = SystemInformation.KeyboardDelay;

        public KeyBindSender(WowWindow wowWindow, IKeyboardSimulator simulator,
            SavedVariablesParser parser, ILogger logger)
        {
            _wowWindow = wowWindow;
            _simulator = simulator;
            _logger = logger;

            _keyBindingDictionary = parser.GetByName("keybindings").ParseKeyBind();
            _logger.Information("Loaded Key Bindings from Saved Variables..");


            _logger.Debug("{@KeysByBinding}", _keyBindingDictionary);
        }

        public async Task SimulateKeyPress(KeyBinding kb)
        {
            _logger.Information("Pressed {@Keys} for {KeyBinding}", _keyBindingDictionary[kb].KeyList, kb);
            await SimulateKeyPress(_keyBindingDictionary[kb].KeyList);
        }

        public async Task SimulateKeyPress(IEnumerable<VirtualKeyCode> keys)
        {
            foreach (var key in keys) await SimulateKeyDown(key);

            await Sleep(SleepTime);

            foreach (var key in keys.Reverse()) await SimulateKeyUp(key);
        }


        
        public async Task SimulateKeyUp(VirtualKeyCode keyCode)
        {
            _wowWindow.SetForeground();
            _simulator.KeyUp(keyCode);
        }

        public async Task SimulateHoldKey(VirtualKeyCode keyCode, CancellationToken ct)
        {
            _wowWindow.SetForeground();
            _simulator.KeyPress(keyCode);
            await Sleep(SystemInformation.KeyboardDelay);
            while (!ct.IsCancellationRequested)
            {
                _wowWindow.SetForeground();
                _simulator.KeyUp(keyCode);
                await Sleep(SystemInformation.KeyboardSpeed);
            }
        }

        public async Task SimulateKeyDown(VirtualKeyCode keyCode)
        {
            _wowWindow.SetForeground();
            _simulator.KeyDown(keyCode);
        }


        private static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}