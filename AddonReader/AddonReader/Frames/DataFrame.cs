#region

using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.VisualBasic.FileIO;

using TenBot.AddonReader;
using TenBot.Game.WowEntities;

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

        public enum FieldName
        {

        }

        
    
        private BitmapProvider? _bitmapProvider;

        public DataFrame(int index, Point p, string name, BitmapProvider? bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;
            Index = index;
            Point = p;
            Name = name;

            var unitName  = Name.Split("-");

            if (unitName.Length > 0)
            {
                var unitField = Enum.TryParse(unitName[0], true, out Unit.UnitField field) ? field: Unit.UnitField.None;
            }
        }
        public DataFrame(string s)
        {
            Parse(s);
        }

        

        public Color Color => _bitmapProvider?.GetBitmap().GetPixel(Point.X, Point.Y) ?? Color.Empty;

        public int Index { get; }

        public string Name { get; }

        public Point Point { get; set; }

      
        public static DataFrame Parse(string value)
        {
            var paramStrings = value.Split(";");
            var index = int.Parse(paramStrings[0]);
            var name = paramStrings[1];
            var p = Point.Empty;

            return new DataFrame(index, p, name, null);
        }
    }
}