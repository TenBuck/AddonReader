using System;
using System.Collections.Generic;
using System.Drawing;

namespace TenBot
{
    public class AddonConfig
    {
        public AddonConfig(IEnumerable<string> configEnumerable)
        {
            foreach (var config in configEnumerable)
            {
                var data = config.Split(";");
                var property = typeof(AddonConfig).GetProperty(data[0]);

                if (property == null) continue;

                var value = Convert.ChangeType(data[1], property.PropertyType);

                property.SetValue(this, value);
            }
        }

        public Rectangle AddonRectangle => new Rectangle(0, 0, Columns * CellDistance, Rows * CellDistance);

        public int BoxCount { get; private set; }

        public int CellDistance => CellSpacing + CellSize;

        public int CellSize { get; private set; }

        public int CellSpacing { get; set; }

        public int Columns => BoxCount > MaxColumns ? MaxColumns : BoxCount;
        public int MaxColumns { get; private set; }

        public int MaxRow { get; private set; }

        public int Rows => BoxCount / MaxColumns + 1;

        public int StringMaxChar { get; private set; }
        
        public int ErrorInt { get; set; }


        // Lua index starts at 1, 
        public Point GetPointFromIndex(int index)
        {
            var xOffset = CellSize / 2 - 1;
            var yOffset = CellSize / 2 - 1;
            var x = xOffset + ((index - 1) % MaxColumns) * CellDistance;
            var y = yOffset + (index - 1) / MaxColumns * CellDistance;


            return new Point(x, y);
        }
    }
}