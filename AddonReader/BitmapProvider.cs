using System.Diagnostics;
using System.Drawing;

namespace AddonReader
{
    public class BitmapProvider
    {
        private const int Timeout = 500;

        private Bitmap bitmap;
        private Rectangle bitmapRectangle;

        private readonly Stopwatch stopWatch = new Stopwatch();


        public BitmapProvider(Rectangle rectangle)
        {
            bitmapRectangle = rectangle;
        }

        public Bitmap GetBitmap()
        {
            if (stopWatch.IsRunning && stopWatch.ElapsedMilliseconds <= Timeout) return bitmap;
            bitmap = new Bitmap(bitmapRectangle.Width, bitmapRectangle.Height);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(bitmapRectangle.X, bitmapRectangle.Y, 0, 0, bitmap.Size);
            }

            stopWatch.Restart();

            return bitmap;
        }
    }
}