using System.Drawing;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using PInvoke;
using TenBot.Extensions;

namespace TenBot
{
    public class MouseServices
    {
        private const int SleepTime = 100;
        private readonly IMouseSimulator _mouseSimulator;
        private readonly WowWindow _wowWindow;

        public MouseServices(IMouseSimulator mouseSimulator, WowWindow _wowWindow)
        {
            this._wowWindow = _wowWindow;
            _mouseSimulator = mouseSimulator;
        }

        public async Task RightClickMiddle(double radians)
        {
            _wowWindow.SetForeground();
            var pixel = radians / 0.03927 * 10;

            var p = _wowWindow.ClientRectangle.Middle();
            await SetCursorPos(p);
            _wowWindow.SetForeground();
            await Sleep(100);
            _mouseSimulator.RightButtonDown();
            await Sleep(100);
            _mouseSimulator.MoveMouseBy((int)pixel, 0);
            await Sleep(100);
            _mouseSimulator.RightButtonUp();
            await Sleep(3000);
            
        }

        public async Task LeftClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(SleepTime);
            _mouseSimulator.LeftButtonClick();
            await Sleep(SleepTime);
            await SetCursorPos(Point.Empty);
        }

        public async Task RightClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(SleepTime);
            _mouseSimulator.RightButtonClick();
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