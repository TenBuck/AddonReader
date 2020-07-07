using System.Collections.Generic;

namespace AddonReader
{
    public class AddonReaderMgr
    {
        private readonly Dictionary<string, int> _addonConfig;
        private BitmapProvider _bitmapProvider;


        private readonly List<DataFrame> _frames;

        public AddonReaderMgr(BitmapProvider bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;
        }

        public void Initialize()
        {
        }
    }
}