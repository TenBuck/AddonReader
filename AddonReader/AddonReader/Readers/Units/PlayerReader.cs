using TenBot.AddonReader.Boxes;
using TenBot.AddonReader.Readers.Unit;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class PlayerReader : UnitReader
    {
        public PlayerReader(BoxMgr boxMgr) : base(boxMgr, "player")
        {
        }
        public int Casting => _boxMgr.GetBoxByName(UnitName + "Cast").Color.ToInt();
        public int Durability => _boxMgr.GetBoxByName(UnitName + "durability").Color.ToInt();
        public int Freeslots => _boxMgr.GetBoxByName(UnitName + "freeslots").Color.ToInt();
        public bool IsSpellInRange => _boxMgr.GetBoxByName(UnitName + "IsInRange").Color.ToBool();
    }
}