using System.Diagnostics;
using System.Drawing;

namespace TenBot.AddonReader
{
    public class BitmapProvider
    {
        private readonly WowWindow _wowWindow;
        private const int Timeout = 500;

        private readonly Bitmap _bitmap;

        private readonly Stopwatch _stopWatch = new Stopwatch();
        private Rectangle _rectangle;
        public BitmapProvider(AddonConfigProvider provider, WowWindow _wowWindow)
        {
            this._wowWindow = _wowWindow;
            _rectangle = provider.AddonRectangle;
            _bitmap = new Bitmap(_rectangle.Width, _rectangle.Height);
        }

        public Bitmap GetBitmap()
        {
            if (_stopWatch.IsRunning == false) _rectangle = _wowWindow.ClientToScreen(_rectangle);
            if (_stopWatch.IsRunning && _stopWatch.ElapsedMilliseconds <= Timeout) return _bitmap;

            using (var graphics = Graphics.FromImage(_bitmap))
            {
                graphics.CopyFromScreen(_rectangle.Location, Point.Empty, _bitmap.Size);
                graphics.Dispose();
            }

            _stopWatch.Restart();
            //_bitmap.Save("test.jpg");

            return _bitmap;
        }
    }
}