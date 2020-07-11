using System.Collections.Generic;
using System.Linq;

namespace TenBot.Extensions
{
    public static class BoxListExtensions
    {
        public static string BoxesToString(this List<Box> boxes)
        {
            var value = boxes.Aggregate("", (current, frame) => current + frame.Color.ToChars());

            return value.Split("ÿ", 2)[0];
        }
    }
}