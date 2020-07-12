#region

using System.Drawing;
using Serilog;

#endregion

namespace TenBot.AddonReader.Boxes
{
    public class Box
    {
        public enum DataFrameType
        {
            Int,
            Bool,
            Decimal,
            Chars
        }

        private readonly BitmapProvider? _bitmapProvider;

        public Box(int index, Point p, string name, BitmapProvider? bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;
            Index = index;
            Point = p;
            Name = name;
        }

        public Color Color
        {
            get
            {
                var color = _bitmapProvider?.GetBitmap().GetPixel(Point.X, Point.Y) ?? Color.Empty;
                Log.Logger.Verbose("{Name} at {Point}: {Color}", Name, Point, color);
                return color;
            }
        }

        public int Index { get; }

        public string Name { get; }


        public Point Point { get; set; }


        public static Box Parse(string value)
        {
            var paramStrings = value.Split(";");

            var index = int.Parse(paramStrings[0]);

            var name = paramStrings[2];
            var p = Point.Empty;

            return new Box(index, p, name, null);
        }
    }
}