using System.Collections.Generic;
using TenBot.AddonReader.Boxes;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Readers.ActionBars
{
    public class ActionsReader
    {
        private readonly BoxMgr _boxMgr;
        private readonly ActionBarInMemoryCache _spellCache;
        

        public ActionsReader(BoxMgr boxMgr, ActionBarInMemoryCache spellCache)
        {
            _boxMgr = boxMgr;
            _spellCache = spellCache;
        }
        public double GetSpellRemainingCooldown(int spellId)
        {
            return _boxMgr.GetBoxByName("actionbars-Cooldown_" + (int) _spellCache.GetActionSlot(spellId)).Color.ToInt();
        }
    }
}