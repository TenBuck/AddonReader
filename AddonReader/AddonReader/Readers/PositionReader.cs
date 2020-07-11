using TenBot.AddonReader.Frames;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class PositionReader
    {
        private readonly BoxMgr _boxMgr;
        private readonly string _unitName;

        public PositionReader(BoxMgr boxMgr, string unitName)
        {
            _boxMgr = boxMgr;
            _unitName = unitName + "-Position-";
        }


        public double X => _boxMgr.GetBoxByName(_unitName + "X").Color.ToDouble();
        public double Y => _boxMgr.GetBoxByName(_unitName + "Y").Color.ToDouble();
        public double Facing => _boxMgr.GetBoxByName(_unitName + "Facing").Color.ToDouble();
    }
}