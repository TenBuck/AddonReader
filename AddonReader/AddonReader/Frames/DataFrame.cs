#region

using System.Drawing;

#endregion

namespace TenBot
{
    public class DataFrame
    {
        public enum DataFrameType
        {
            Int,
            Bool,
            Decimal,
            Chars
        }


        private readonly BitmapProvider? _bitmapProvider;

        public DataFrame(int index, Point p, string name, BitmapProvider? bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;
            Index = index;
            Point = p;
            Name = name;
        }

        public Color Color => _bitmapProvider?.GetBitmap().GetPixel(Point.X, Point.Y) ?? Color.Empty;

        public int Index { get; }

        public string Name { get; }

        public Point Point { get; set; }


        public static DataFrame Parse(string value)
        {
            var paramStrings = value.Split(";");

            var index = int.Parse(paramStrings[0]);

            var name = paramStrings[2];
            var p = Point.Empty;

            return new DataFrame(index, p, name, null);
        }
    }
}