using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class UnitReader
    {
        protected readonly BoxMgr _boxMgr;

        public readonly string UnitName;

        public UnitReader(BoxMgr boxMgr, string unitName)
        {
            _boxMgr = boxMgr;
            UnitName = unitName + "-";
        }
        public string Name => _boxMgr.GetBoxListByName(UnitName + "Name").BoxesToString();
        public int Health => _boxMgr.GetBoxByName(UnitName + "Health").Color.ToInt();
        public int HealthMax => _boxMgr.GetBoxByName(UnitName + "HealthMax").Color.ToInt();
        public int Power => _boxMgr.GetBoxByName(UnitName + "Power").Color.ToInt();
        public int PowerMax => _boxMgr.GetBoxByName(UnitName + "PowerMax").Color.ToInt();
        public int Level => _boxMgr.GetBoxByName(UnitName + "Level").Color.ToInt();
        public bool IsDead => _boxMgr.GetBoxByName(UnitName + "IsDead").Color.ToBool();
        public int MovingSpeed => _boxMgr.GetBoxByName(UnitName + "MovingSpeed").Color.ToInt();
    }
}