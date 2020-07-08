using System.Drawing;
using System.Threading;

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

        private readonly BitmapProvider? _bitmapProvider;

        public DataFrame(int index, Point p, string name, BitmapProvider? bitmapProvider)
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

        public static DataFrame Parse(string value)
        {
            var paramStrings = value.Split(";");
            var index = int.Parse(paramStrings[0]);
            var name = paramStrings[1];
            var p = System.Drawing.Point.Empty;

            return new DataFrame(index, p, name, null);
        }

        
    }
}