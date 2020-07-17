#region

using System.Drawing;
using Serilog;
using TenBot.Extensions;

#endregion

namespace TenBot.AddonReader.Boxes
{
    public class Box
    {
        public const int MaxInt = 16777215;

        private readonly BitmapProvider? _bitmapProvider;

        public Box(int index, Point p, string name, BitmapProvider? bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;
            Index = index;
            Point = p;
            Name = name;
        }

        public Box(string value)
        {
            var paramStrings = value.Split(";");

            Index = int.Parse(paramStrings[0]);

            Name = paramStrings[2];
            Point = Point.Empty;
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

        public bool HasValue()
        {
            return !this.ToInt().Equals(MaxInt);
        }
    }
}