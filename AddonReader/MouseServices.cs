using System.Drawing;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using PInvoke;

namespace TenBot
{
    public class MouseServices
    {
        private readonly IMouseSimulator _mouseSimulator;
        private readonly WowWindow _wowWindow;
        private int SleepTime = 100;
        public MouseServices(IInputSimulator inputSimulator, WowWindow _wowWindow)
        {
            this._wowWindow = _wowWindow;
            _mouseSimulator = inputSimulator.Mouse;
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

        private async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }
    }
}