using System.Collections.Generic;
using Serilog;
using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class AuraReader
    {
        private readonly ILogger _logger;

        private readonly BoxMgr _boxMgr;

        public int BuffCount => _boxMgr.GetBoxListByName("auras-buff").FindAll(s => s.Color.ToInt() != 16777215).Count;
        public int DebuffCount => _boxMgr.GetBoxListByName("auras-debuff").FindAll(s => s.Color.ToInt() != 16777215).Count;

        public List<int> Buffs => _boxMgr.GetBoxListByName("auras-buff").ConvertAll(s => s.Color.ToInt())
            .FindAll(s => s != 16777215);


        public List<int> Debuffs => _boxMgr.GetBoxListByName("auras-debuff").ConvertAll(s => s.Color.ToInt())
            .FindAll(s => s != 16777215);


        public AuraReader(ILogger logger, BoxMgr boxMgr)
        {
            _logger = logger;
            _boxMgr = boxMgr;
        }

    }
}