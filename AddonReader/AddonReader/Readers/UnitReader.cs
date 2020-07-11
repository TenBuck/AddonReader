using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class UnitReader
    {
        protected BoxMgr _boxMgr;

        protected readonly string _unitName;

        public UnitReader(BoxMgr boxMgr, string unitName)
        {
            _boxMgr = boxMgr;
            _unitName = unitName + "-";
        }

        public string Name => _boxMgr.GetBoxListByName(_unitName + "Name").BoxesToString();
        public int Health => _boxMgr.GetBoxByName(_unitName + "Health").Color.ToInt();
        public int HealthMax => _boxMgr.GetBoxByName(_unitName + "HealthMax").Color.ToInt();
        public int Power => _boxMgr.GetBoxByName(_unitName + "Power").Color.ToInt();
        public int PowerMax => _boxMgr.GetBoxByName(_unitName + "PowerMax").Color.ToInt();
        public int Level => _boxMgr.GetBoxByName(_unitName + "Level").Color.ToInt();
        public bool IsDead => _boxMgr.GetBoxByName(_unitName + "IsDead").Color.ToBool();
        public int MovingSpeed => _boxMgr.GetBoxByName(_unitName + "MovingSpeed").Color.ToInt();
    }
}