using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using PInvoke;
using static PInvoke.User32;

namespace TenBot
{
    public class KeyBindSender
    {
        private const int SleepTime = 45;

        private readonly InputSimulator _simulator;
        private readonly WowWindow _wowWindow;

        public KeyBindSender(WowWindow wowWindow, InputSimulator simulator)
        {
            _wowWindow = wowWindow;
            _simulator = simulator;
        }

        public async Task SimulateKeyPress(IEnumerable<VirtualKeyCode> keys)
        {
            foreach (var key in keys) await SimulateKeyDown(key);

            await Sleep(SleepTime);

            foreach (var key in keys.Reverse()) await SimulateKeyUp(key);
        }

        public async Task SimulateKeyPress(VirtualKeyCode key)
        {
            await SimulateKeyDown(key);
            await Sleep(SleepTime);
            await SimulateKeyDown(key);
        }

        public async Task SimulateKeyUp(VirtualKeyCode keyCode)
        {
            _wowWindow.SetForeground();
            _simulator.Keyboard.KeyUp(keyCode);
            await Sleep(SleepTime);
        }

        public async Task SimulateKeyDown(VirtualKeyCode keyCode)
        {
            _wowWindow.SetForeground();
            _simulator.Keyboard.KeyDown(keyCode);
            await Sleep(SleepTime);
        }

        public async Task LeftClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(SleepTime);
            _simulator.Mouse.LeftButtonClick();
            await Sleep(SleepTime);
            await SetCursorPos(Point.Empty);
        }

        public async Task RightClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(SleepTime);
            _simulator.Mouse.RightButtonClick();
            await Sleep(SleepTime);
            await SetCursorPos(Point.Empty);
        }

        public async Task SetCursorPos(Point point)
        {
            _wowWindow.SetForeground();
            await Sleep(SleepTime);
            User32.SetCursorPos(point.X, point.Y);
            await Sleep(SleepTime);
        }

        public static Point GetCursorPos()
        {
            return User32.GetCursorPos();
        }
        
        private static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}