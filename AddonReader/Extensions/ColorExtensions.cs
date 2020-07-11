using System.Drawing;

namespace TenBot.Extensions
{
    public static class ColorExtensions
    {
        public static string ToChars(this Color color)
        {
            var chars = "";
            chars += ((char)color.R).ToString();
            chars += ((char)color.G).ToString();
            chars += ((char)color.B).ToString();
            return chars;
        }

        public static int ToInt(this Color color)
        {
            return color.R + color.G * 256 + color.B * 256 * 256;
        }

        public static double ToDouble(this Color color)
        {
            return color.ToInt() / 10000.0;
        }


        // TODO: allow multiple bools in one box
        public static bool ToBool(this Color color)
        {
            return color.ToInt() == 1;
        }
    }
}