using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class PlayerReader : UnitReader
    {
        public PlayerReader(BoxMgr boxMgr,
            PositionReader positionReader) : base(boxMgr, "player")
        {
            Position = positionReader;
        }

        public PositionReader Position { get; }

        public int Casting => _boxMgr.GetBoxByName(UnitName + "Cast").ToInt();
        public int Durability => _boxMgr.GetBoxByName(UnitName + "durability").ToInt();
        public int Freeslots => _boxMgr.GetBoxByName(UnitName + "freeslots").ToInt();

        public bool IsInCombat => _boxMgr.GetBoxByName(UnitName + "IsInCombat").ToBool();
    }
}