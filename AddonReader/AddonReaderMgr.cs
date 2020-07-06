using System.Collections.Generic;

namespace AddonReader
{
    public class AddonReaderMgr
    {
        private BitmapProvider bitmapProvider;


        private List<DataFrame> frames;
        private Dictionary<string, int> addonConfig;
        public AddonReaderMgr(BitmapProvider bitmapProvider)
        {
            this.bitmapProvider = bitmapProvider;


        }

        public void Initialize()
        {
            addonConfig = new Dictionary<string, int>()
            {
                ["MaxRow"] = 5,
                ["CellSize"] = 8,
                ["StringMaxChar"] = 21,
                ["MaxColumn"] = 50,
                ["CellSpacing"] = 0,
                ["BoxCount"] = 83,
            };


        }
    }
}
