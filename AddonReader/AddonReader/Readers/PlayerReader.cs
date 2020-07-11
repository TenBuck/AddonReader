using System;
using System.Collections.Generic;
using System.Text;
using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{ 
    public class PlayerReader : UnitReader
    {
       
       
        private PositionReader _positionReader;


        public PlayerReader(BoxMgr boxMgr) : base(boxMgr, "player")
        {
           _positionReader = new PositionReader(_boxMgr, _unitName);
        }

        public int Casting  => _boxMgr.GetBoxByName(_unitName + "Cast").Color.ToInt();
        public int Durability => _boxMgr.GetBoxByName(_unitName + "durability").Color.ToInt();
        public int Freeslots => _boxMgr.GetBoxByName(_unitName + "freeslots").Color.ToInt();


    }
}
