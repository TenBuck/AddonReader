using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class TargetReader : UnitReader
    {
        public TargetReader(BoxMgr boxMgr, string unitName ) : base(boxMgr, unitName)
        {
            
        }
        public int NpcId => _boxMgr.GetBoxByName(UnitName + "Id").Color.ToInt();
        public int Type => _boxMgr.GetBoxByName(UnitName + "Type").Color.ToInt();
    }
}