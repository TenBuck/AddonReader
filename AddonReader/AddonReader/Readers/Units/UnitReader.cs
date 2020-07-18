using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class UnitReader
    {
        protected readonly BoxMgr _boxMgr;

        public readonly string UnitName;

        protected UnitReader(BoxMgr boxMgr, string unitName)
        {
            _boxMgr = boxMgr;
            UnitName = unitName + "-";
        }

        public string Name => _boxMgr.GetBoxListByName(UnitName + "Name").BoxesToString();
        public int Health => _boxMgr.GetBoxByName(UnitName + "Health").ToInt();
        public int HealthPercent => (int)((double)Health / (HealthMax != 0 ? HealthMax : int.MaxValue) * 100.0);
        public int HealthMax => _boxMgr.GetBoxByName(UnitName + "HealthMax").ToInt();
        public int Power => _boxMgr.GetBoxByName(UnitName + "Power").ToInt();
        public int PowerPercent => (int) ((double)Power / (PowerMax != 0 ? PowerMax : int.MaxValue) * 100.0);
        public int PowerMax => _boxMgr.GetBoxByName(UnitName + "PowerMax").ToInt();
        public int Level => _boxMgr.GetBoxByName(UnitName + "Level").ToInt();
        public bool IsDead => Health == 0; //_boxMgr.GetBoxByName(UnitName + "IsDead").ToBool();
        public int MovingSpeed => _boxMgr.GetBoxByName(UnitName + "MovingSpeed").ToInt();
    }
}