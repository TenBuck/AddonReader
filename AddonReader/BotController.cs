using System.Drawing;

using Serilog;

using TenBot.AddonReader;
using TenBot.AddonReader.Frames;
using TenBot.Game.WowEntities;
using TenBot.Game.WowTypes;

namespace TenBot
{
    public class BotController
    {
        
        private AddonReaderMgr _addonReaderMgr;
        
        private WowWindow _wowWindow;

        public BotController(AddonReaderMgr addonReaderMgr,  WowWindow wowWindow)
        {
            
            _addonReaderMgr = addonReaderMgr;
            _wowWindow = wowWindow;
        }
        public void Start()
        {
            while (true)
            {
                _wowWindow.SetForeground();
                _wowWindow.MoveWindow(Point.Empty);

                var rect = _wowWindow.Rectangle;
                var clientRect = _wowWindow.ClientRectangle;
                var player = new Unit(UnitId.Player, _addonReaderMgr._frames);
                Log.Logger.Information("Health is {Health}");
            };


        }
    }
}