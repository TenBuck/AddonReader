using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class PlayerReader : UnitReader
    {
        private PositionReader _positionReader;


        public PlayerReader(BoxMgr boxMgr) : base(boxMgr, "player")
        {
            _positionReader = new PositionReader(_boxMgr, UnitName);
        }

        public int Casting => _boxMgr.GetBoxByName(UnitName + "Cast").Color.ToInt();
        public int Durability => _boxMgr.GetBoxByName(UnitName + "durability").Color.ToInt();
        public int Freeslots => _boxMgr.GetBoxByName(UnitName + "freeslots").Color.ToInt();
    }
}