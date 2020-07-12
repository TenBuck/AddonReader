using System.Collections.Generic;
using System.Linq;
using Serilog;
using TenBot.AddonReader.Boxes;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class AuraReader
    {
        private readonly BoxMgr _boxMgr;
        private readonly ILogger _logger;
        public AuraReader(ILogger logger, BoxMgr boxMgr)
        {
            _logger = logger;
            _boxMgr = boxMgr;
        }

        public int BuffCount =>
            _boxMgr.GetBoxListByName("aura-buff-count").FindAll(s => s.Color.ToInt() != 16777215).Count;

        public int DebuffCount =>
            _boxMgr.GetBoxListByName("aura-debuff-count")
                .Count(s => s.Color.ToInt() != 16777215);

        public List<int> Buffs => _boxMgr.GetBoxListByName("aura-buff_").ConvertAll(s => s.Color.ToInt())
            .Where(s => s != 16777215).ToList();


        public List<int> Debuffs => _boxMgr.GetBoxListByName("aura-debuff_").ConvertAll(s => s.Color.ToInt())
            .Where(s => s != 16777215).ToList();
    }
}