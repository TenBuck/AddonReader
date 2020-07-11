using System;
using System.Drawing;
using Serilog;
using TenBot.AddonReader.SavedVariables;

namespace TenBot
{
    public class AddonConfigProvider
    {
        private readonly ILogger _logger;

        public AddonConfigProvider(SavedVariablesParser parser, ILogger logger)
        {
            _logger = logger;

            var fields = parser.GetGlobalByName("addonConfig").Fields;

            foreach (var config in fields)
            {
                var data = config.Split(";");
                var property = typeof(AddonConfigProvider).GetProperty(data[0]);

                if (property == null) continue;

                var value = Convert.ChangeType(data[1], property.PropertyType);

                property.SetValue(this, value);
            }

            _logger.Information("Loaded addon settings from saved variables");
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
            var x = xOffset + (index - 1) % MaxColumns * CellDistance;
            var y = yOffset + (index - 1) / MaxColumns * CellDistance;


            return new Point(x, y);
        }
    }
}