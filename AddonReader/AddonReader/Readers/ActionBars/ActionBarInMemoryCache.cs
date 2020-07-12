using System.Collections.Generic;
using System.Linq;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Readers.ActionBars
{
    public class ActionBarInMemoryCache
    {
        public Dictionary<int, ActionSlot> SpellActionBarById = new Dictionary<int, ActionSlot>();


        // TODO: STOP pasting duplicates
        public ActionBarInMemoryCache(SavedVariablesParser parser)

        {
            var luaItems = parser.GetByName("actionBars").Fields.ConvertAll(ActionBarItem.Parse).ToList();


            foreach (var actionBarItem in luaItems)
                if (!SpellActionBarById.ContainsKey(actionBarItem.SpellId))
                    SpellActionBarById.Add(actionBarItem.SpellId, actionBarItem.ActionSlot);
        }
    }
}