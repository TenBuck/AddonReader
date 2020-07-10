using System.Diagnostics;
using System.Drawing;

namespace TenBot
{
    public class BitmapProvider
    {
        private const int Timeout = 500;


        private readonly Stopwatch _stopWatch = new Stopwatch();

        private readonly WowWindow _wowWindow;

        private Bitmap _bitmap;
        public BitmapProvider(WowWindow wowWindow)
        {
            _wowWindow = wowWindow;
        }

        public Rectangle AddonRectangle { get; set; }

        private Rectangle ClientRectangle => _wowWindow.ClientToScreen(AddonRectangle);

        public Bitmap GetBitmap()
        {
            if (_stopWatch.IsRunning && _stopWatch.ElapsedMilliseconds <= Timeout) return _bitmap;
            _bitmap = new Bitmap(AddonRectangle.Width, AddonRectangle.Height);
            using (var graphics = Graphics.FromImage(_bitmap))
            {
                
                graphics.CopyFromScreen(ClientRectangle.Location, Point.Empty, _bitmap.Size);
            }

            _bitmap.Save("test.jpg");
            _stopWatch.Restart();

            return _bitmap;
        }
    }
}