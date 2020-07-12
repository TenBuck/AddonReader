using System.Collections.Generic;
using TenBot.AddonReader.Boxes;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Readers.ActionBars
{
    public class ActionsReader
    {
        private readonly BoxMgr _boxMgr;
        private readonly Dictionary<int, ActionSlot> _actionSlotBySpellId;

        public ActionsReader(BoxMgr boxMgr, ActionBarInMemoryCache spellCache)
        {
            _boxMgr = boxMgr;
            _actionSlotBySpellId = spellCache.SpellActionBarById;
        }

        public double GetSpellRemainingCooldown(int spellId)
        {
            return _boxMgr.GetBoxByName("actionbars-SpellInfo_" + (int) _actionSlotBySpellId[spellId]).Color.ToInt();
        }
    }
}