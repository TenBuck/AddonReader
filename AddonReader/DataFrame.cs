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

        private readonly BitmapProvider bitmapProvider;

        public DataFrame(Point p, int index, string name, BitmapProvider bitmapProvider)
        {
            Point = p;
            Index = index;
            Name = name;
        }

        public DataFrame(int index, string name, BitmapProvider bitmapProvider)
        {
            Index = index;
            Name = name;
            this.bitmapProvider = bitmapProvider;
        }

        public Color Color => bitmapProvider.GetBitmap().GetPixel(Point.X, Point.Y);
        public Point Point { get; }
        public int Index { get; }

        public string Name { get; }


        
    }
}