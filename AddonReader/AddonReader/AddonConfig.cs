using System;
using System.Collections.Generic;
using System.Drawing;

namespace TenBot
{
    public class AddonConfig
    {
        private bool isConfig = false;

        public AddonConfig(IEnumerable<string> configEnumerable)
        {
            var properties = typeof(AddonConfig).GetProperties();
            foreach (var config in configEnumerable)
            {
                var data = config.Split(";");
                var property = typeof(AddonConfig).GetProperty(data[0]);

                if (property == null) continue;

                var value = Convert.ChangeType(data[1], property.PropertyType);

                property.SetValue(this, value);
            }

            isConfig = true;
        }

       
        public Rectangle AddonRectangle => new Rectangle(0, 0, Columns * CellDistance, Rows * CellDistance);

        public int BoxCount { get; private set; }

        public int CellDistance => CellSpacing + CellSize;

        public int CellSize { get; private set; }

        public int CellSpacing { get; set; }

        public int Columns => BoxCount > MaxColumns ? MaxColumns : BoxCount;

        public List<KeyValuePair<string, int>> Configs { get; }

        public int MaxColumns { get; private set; }

        public int MaxRow { get; private set; }

        public int Rows => BoxCount / (MaxColumns + 1);

        public int StringMaxChar { get; private set; }

        public Point GetPointFromIndex(int index)
        {
            var xOffset = CellSize / 2;
            var yOffset = CellSize / 2;
            var x = xOffset + (index % (MaxColumns)) * (CellDistance);
            var y = yOffset + (index / MaxColumns) * (CellDistance);


            return new Point(x, y);
        }
    }
}