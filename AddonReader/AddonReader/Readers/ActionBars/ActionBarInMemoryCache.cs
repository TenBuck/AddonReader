using System.Collections.Generic;
using System.Linq;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Readers.ActionBars
{
    public class ActionBarInMemoryCache
    {
        private List<ActionBarItem> _actionBarItemList;


        // TODO: STOP pasting duplicates
        public ActionBarInMemoryCache(SavedVariablesParser parser)
        {
            _actionBarItemList = parser.GetByName("actionBars").Fields.ConvertAll(ActionBarItem.Parse).ToList();
        }

        public ActionSlot GetActionSlot(int spellId)
        {
            return _actionBarItemList.First(s => s.SpellId == spellId).ActionSlot;
        }

        public string GetSpellName(ActionSlot actionSlot)
        {
            return _actionBarItemList.First(s => s.ActionSlot == actionSlot).SpellName;
        }
    }
}