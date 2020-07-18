using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class TargetReader : TargetBase
    {
        public TargetReader(BoxMgr boxMgr) : base(boxMgr, "target")
        {
        }

        public bool IsTapped => _boxMgr.GetBoxByName("isTapped").ToBool();
    }
}