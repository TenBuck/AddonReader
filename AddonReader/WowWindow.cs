using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using GregsStack.InputSimulatorStandard.Native;
using PInvoke;
using TenBot.Extensions;

namespace TenBot
{
    public class WowWindow
    {
        private readonly float _dpiX;
        private readonly float _dpiY;

        private readonly InputSimulator _simulator = new InputSimulator();

        public WowWindow()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                _dpiX = graphics.DpiX;
                _dpiY = graphics.DpiY;
                graphics.Dispose();
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
                (int) (rect.X + p.X),
                (int) (rect.Y + p.Y),
                (int) rect.Width,
                (int) rect.Height);
        }

        public void MoveWindow(Point point)
        {
            User32.SetWindowPos(Handle, (IntPtr) 0, 0, 0, 0, 0, User32.SetWindowPosFlags.SWP_NOSIZE);
        }

        private async Task Sleep(int ms)
        {
            await Task.Delay(ms);
        }

        private static Process? GetProcess(string name)
        {
            var processes = Process.GetProcessesByName(name);

            return processes.Length < 1 ? null : processes[0];
        }
    }
}