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
            _boxMgr.GetBoxByName("aura-buff-count").ToInt();

        public int DebuffCount => _boxMgr.GetBoxByName("aura-debuff-count").ToInt();
         

        public List<int> Buffs => _boxMgr.GetBoxListByName("aura-buff_")
            .Where(box => box.HasValue())
            .Select(box => box.ToInt()).ToList();


        public List<int> Debuffs => _boxMgr.GetBoxListByName("aura-debuff_")
            .Where(box => box.HasValue())
            .Select(box => box.ToInt()).ToList();
    }
}