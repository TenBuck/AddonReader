using System.Drawing;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using PInvoke;

namespace TenBot
{
    public class KeyBindSender


    {
        private InputSimulator _simulator;

        public async Task SimulateKeyPress(VirtualKeyCode keyCode)
        {
            _simulator.Keyboard.KeyPress(keyCode);
            await Sleep(50);
        }

        public async Task SimulateKeyUp(VirtualKeyCode keyCode)
        {
            _simulator.Keyboard.KeyUp(keyCode);
            await Sleep(50);
        }

        public async Task SimulateKeyDown(VirtualKeyCode keyCode)
        {
            _simulator.Keyboard.KeyDown(keyCode);
            await Sleep(50);
        }

        public async Task LeftClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(100);
            _simulator.Mouse.LeftButtonClick();
            await Sleep(100);
            await SetCursorPos(Point.Empty);
        }

        public async Task RightClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(100);
            _simulator.Mouse.RightButtonClick();
            await Sleep(100);
            await SetCursorPos(Point.Empty);
        }

        public async Task SetCursorPos(Point point)
        {
            User32.SetCursorPos(point.X, point.Y);
            await Sleep(50);
        }

        public static Point GetCursorPos()
        {
            return User32.GetCursorPos();
        }

        private async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}