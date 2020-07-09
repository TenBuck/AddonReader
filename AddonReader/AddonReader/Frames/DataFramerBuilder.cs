namespace TenBot
{
    public class DataFramerBuilder
    {
        private AddonConfig _addonConfig;
        private readonly BitmapProvider _bitmapProvider;

        public DataFramerBuilder(AddonConfig config, BitmapProvider bitmapProvider)
        {
            _addonConfig = config;
            _bitmapProvider = bitmapProvider;
        }

        public DataFrame BuildFromParse(string dataFrame)
        {
            var paramStrings = dataFrame.Split(";");
            var index = int.Parse(paramStrings[0]);
            var name = paramStrings[1];
            var p = _addonConfig.GetPointFromIndex(index);

            return new DataFrame(index, p, name, _bitmapProvider);
        }
    }
}