using System.Collections.Generic;
using System.Linq;
using TenBot.AddonReader.Boxes;

namespace TenBot.Extensions
{
    public static class BoxExtensions
    {
        public static string BoxesToString(this List<Box> boxes)
        {
            var value = boxes.Aggregate("", (current, frame) => current + frame.Color.ToChars());

            return value.Split("ÿ", 2)[0];
        }

        public static string ToChars(this Box box)
        {
            var chars = "";
            chars += ((char) box.Color.R).ToString();
            chars += ((char) box.Color.G).ToString();
            chars += ((char) box.Color.B).ToString();
            return chars;
        }

        public static int ToInt(this Box box)
        {
            return box.Color.R + box.Color.G * 256 + box.Color.B * 256 * 256;
        }

        public static double ToDouble(this Box box)
        {
            return box.Color.ToInt() / 10000.0;
        }


        // TODO: allow multiple bools in one box
        public static bool ToBool(this Box box)
        {
            return box.Color.ToBool();
        }
    }
}