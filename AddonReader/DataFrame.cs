using System.Drawing;

namespace AddonReader
{
    public class DataFrame
    {
        public enum FrameType
        {
            Int,
            Bool,
            Decimal,
            Chars
        }

        private readonly BitmapProvider _bitmapProvider;

        public DataFrame(int index, Point p, string name, BitmapProvider bitmapProvider)
        {
            Index = index;
            Point = p;
            Name = name;
            _bitmapProvider = bitmapProvider;
        }

        public Color Color => _bitmapProvider.GetBitmap().GetPixel(Point.X, Point.Y);
        public Point Point { get; }
        public int Index { get; }
        public string Name { get; }

        
    }
}