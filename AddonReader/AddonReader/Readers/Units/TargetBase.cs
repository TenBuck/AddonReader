using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class TargetBase : UnitReader
    {
        public TargetBase(BoxMgr boxMgr, string unitName) : base(boxMgr, unitName)
        {


        }
        public int NpcId => _boxMgr.GetBoxByName(UnitName + "Id").Color.ToInt();
        public int Type => _boxMgr.GetBoxByName(UnitName + "Type").Color.ToInt();
        public bool IsEnemy => _boxMgr.GetBoxByName(UnitName + "IsEnemy").Color.ToBool();
        public int Range => _boxMgr.GetBoxByName(UnitName + "Range").Color.ToInt();

        public bool Exists => Name != "";
    }
}