using System;
using System.Drawing;

using Serilog;

using TenBot.AddonReader;
using TenBot.AddonReader.Frames;
using TenBot.AddonReader.Readers;


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



                var unit = new UnitReader(_addonReaderMgr._frames);




                Console.WriteLine((unit.Health));
                Console.WriteLine((unit.PowerMax));
                Console.WriteLine((unit.Power));
                Console.WriteLine((unit.Health));

                Console.WriteLine(unit.Health.ToString(), unit.HealthMax.ToString(), unit.Power.ToString(),
                    unit.PowerMax.ToString(), unit.Level.ToString(), unit.MovingSpeed.ToString());
            };


        }
    }
}