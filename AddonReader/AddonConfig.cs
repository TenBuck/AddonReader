using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;

namespace AddonReader.Data
{
    public class AddonConfig 
    {
        public int MaxRow { get; private set; }
        public int MaxColumn { get; private set; }
        public int CellSize { get; private set; }
        public int StringMaxChar { get; private set; }

        public int CellSpacing { get; set; }
        public int BoxCount { get; private set; }

        public int Rows => BoxCount / MaxColumn + 1;
        public int Columns => (BoxCount > MaxColumn) ? MaxColumn : BoxCount;

        public Point GetPointFromIndex(int index)
        {
            var columns = (BoxCount > MaxColumn) ? MaxColumn : BoxCount;
            var rows = BoxCount % 50 + 1;
            return new Point(columns, rows);
        }

        public Rectangle AddonRectangle => new Rectangle(0, 0, Columns * CellDistance, (Rows * CellDistance));
        public int CellDistance => CellSpacing + CellSize;


        public List<KeyValuePair<string, int>> Configs { get; }
        public AddonConfig(IEnumerable<KeyValuePair<string, string>> configsDictionary)
        {
            var properties = typeof(AddonConfig).GetProperties();
            foreach (KeyValuePair<string, string> kvp in configsDictionary)
            {
                var property = typeof(AddonConfig).GetProperty(kvp.Key);

                if (property == null) continue;

                var value = Convert.ChangeType(kvp.Value, property.PropertyType);

                property.SetValue(this,value);

            }
        }

       
        
    }
}