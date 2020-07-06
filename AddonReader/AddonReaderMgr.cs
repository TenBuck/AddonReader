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
            


        }
    }
}
