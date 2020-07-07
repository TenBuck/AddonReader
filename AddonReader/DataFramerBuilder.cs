using AddonReader.Data;

namespace AddonReader
{
    public class DataFramerBuilder
    {
        private AddonConfig _config;
        private BitmapProvider _bitmapProvider;
        public DataFramerBuilder(AddonConfig config, BitmapProvider bitmapProvider)
        {
            _config = config;
        }

        public DataFrame BuildFromParse(string dataFrame)
        {
            var paramStrings = dataFrame.Split(";");
            var index = int.Parse(paramStrings[0]);
            var name = paramStrings[1];
            var p = _config.GetPointFromIndex(index);

            return new DataFrame(index, p, name, _bitmapProvider);

        }
    }
}