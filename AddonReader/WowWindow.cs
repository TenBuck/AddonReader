using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using PInvoke;
using Serilog;
using TenBot.Extensions;

namespace TenBot
{
    public class WowWindow
    {
        private readonly ILogger logger;
        private readonly InputSimulator simulator = new InputSimulator();

        private readonly float _dpiX;
        private readonly float _dpiY;

        public WowWindow()
        {
            this.logger = logger;

            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                _dpiX = graphics.DpiX;
                _dpiY = graphics.DpiY;
            }
        }

        public Process? Process => GetProcess("WowClassic");

        public IntPtr Handle => Process?.MainWindowHandle ?? IntPtr.Zero;

        public Rectangle Rectangle
        {
            get
            {
                User32.GetWindowRect(Handle, out var rect);

                return rect.ToRectangle();
            }
        }

        public Rectangle ClientRectangle
        {
            get
            {
                User32.GetClientRect(Handle, out var rect);
                return rect.ToRectangle();
            }
        }


        public async Task SimulateKeyPress(VirtualKeyCode keyCode)
        {
            simulator.Keyboard.KeyPress(keyCode);
            await Sleep(50);
        }

        public async Task SimulateKeyUp(VirtualKeyCode keyCode)
        {
            simulator.Keyboard.KeyUp(keyCode);
            await Sleep(50);
        }

        public async Task SimulateKeyDown(VirtualKeyCode keyCode)
        {
            simulator.Keyboard.KeyDown(keyCode);
            await Sleep(50);
        }

        public async Task LeftClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(100);
            simulator.Mouse.LeftButtonClick();
            await Sleep(100);
            await SetCursorPos(Point.Empty);
        }

        public async Task RightClickMouse(Point p)
        {
            await SetCursorPos(p);
            await Sleep(100);
            simulator.Mouse.RightButtonClick();
            await Sleep(100);
            await SetCursorPos(Point.Empty);
        }

        public async Task SetCursorPos(Point point)
        {
            User32.SetCursorPos(point.X, point.Y);
            await Sleep(50);
        }

        public Point GetCursorPos(Point point)
        {
            return User32.GetCursorPos();
        }

        public void SetForeground()
        {
            User32.SetForegroundWindow(Handle);
        }

        public Point GetClientOriginPoint()
        {
            POINT p = new Point(0, 0);
            User32.ClientToScreen(Handle, ref p);

            var point = new Point(p.x, p.y);
            return point;
        }

        public Rectangle ClientToScreen(Rectangle rect)
        {
            var p = GetClientOriginPoint();


            return new Rectangle(
                (int) ((rect.X + p.X) * 2),
                (int) ((rect.Y + p.Y)* 2),
                (int) (rect.Width * 2),
                (int) (rect.Height * 2));
        }

        public void MoveWindow(Point point)
        {
            User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOSIZE);
        }

        private async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }

        private Process? GetProcess(string name)
        {
            var processes = Process.GetProcessesByName(name);

            if (processes == null) logger.Information("Failed to find WoW process...");

            return processes[0];
        }
    }
}