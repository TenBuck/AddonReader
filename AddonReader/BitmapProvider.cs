using System.Diagnostics;
using System.Drawing;

using TenBot.Extensions;

namespace TenBot
{
    public class BitmapProvider
    {
        private const int Timeout = 500;

        private readonly Stopwatch stopWatch = new Stopwatch();

        private Bitmap bitmap;

        public Rectangle AddonRectangle { get => _rectangle; set => _rectangle.Location = (_wowWindow.GetClientOriginPoint()); }

        private WowWindow _wowWindow;
        private Rectangle _rectangle = Rectangle.Empty;

        public BitmapProvider(WowWindow wowWindow)
        {
            _wowWindow = wowWindow;
            
        }


        public Bitmap GetBitmap()
        {
            if (stopWatch.IsRunning && stopWatch.ElapsedMilliseconds <= Timeout) return bitmap;
            bitmap = new Bitmap(_rectangle.Width, _rectangle.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(_rectangle.X, _rectangle.Y, 0, 0, bitmap.Size);
            }

            stopWatch.Restart();

            return bitmap;
        }
    }
}