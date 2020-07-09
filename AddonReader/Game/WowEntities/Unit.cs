using System.Collections.Generic;

using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.Game.WowEntities
{
    public class Unit : WowObject
    {
        public enum UnitField
        {
            Health,
            HealthMax,
            Power,
            PowerMax,
            Level,
            isDead,
            MovingSpeed,
            None
        }

        
        private UnitId _unitId;
        private List<DataFrame> _unitDataFrames;

        public Unit(UnitId unitId, List<DataFrame> dataFrames)
        {
            _unitId = unitId;

            _unitDataFrames = dataFrames.FindAll(s => s.Name.Contains(_unitId.ToString().ToLower()));
        }

        public int Health => 1;
    }
}