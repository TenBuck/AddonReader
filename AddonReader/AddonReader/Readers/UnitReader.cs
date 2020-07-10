using System.Collections.Generic;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class UnitReader
    {
        private readonly List<DataFrame> _frames;

        public UnitReader(List<DataFrame> frames)
        {
            _frames = frames.FindAll(s => s.Name.Contains("player"));
        }

        public int Health => _frames.Find(s => s.Name.Contains("player-Health")).Color.ToInt();

        public int HealthMax => getDataFrame("player-HealthMax").Color.ToInt();


        public int Power => getDataFrame("player-Power").Color.ToInt();
        public int PowerMax => getDataFrame("player-PowerMax").Color.ToInt();
        
        public int Level => getDataFrame("player-Level").Color.ToInt();
        public int IsDead => getDataFrame("player-IsDead").Color.ToInt();
        public int MovingSpeed => getDataFrame("player-MovingSpeed").Color.ToInt();

        private DataFrame getDataFrame(string name)
        {
            return _frames.Find(s => s.Name.Contains(name));
        }
    }
}