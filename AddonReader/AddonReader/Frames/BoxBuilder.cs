using System;

namespace TenBot
{
    public class BoxBuilder
    {
        private readonly AddonConfig _addonConfig;
        private readonly BitmapProvider _bitmapProvider;

        public BoxBuilder(AddonConfig config, BitmapProvider bitmapProvider)
        {
            _addonConfig = config;
            _bitmapProvider = bitmapProvider;
        }

        public Box BuildFromParse(string dataFrame)
        {
            var paramStrings = dataFrame.Split(";");
            var index = int.Parse(paramStrings[0]);
           // var framesId = paramStrings[1];
            var name = paramStrings[2];
            var p = _addonConfig.GetPointFromIndex(index);

            return new Box(index, p, name, _bitmapProvider);
        }
    }
}