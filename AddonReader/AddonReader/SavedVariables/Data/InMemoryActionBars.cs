using System.Collections.Generic;
using System.Linq;
using Serilog;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables
{
    public class InMemoryActionBars : IDataProvider
    {
        private readonly ActionsReader _actionsReader;
        private readonly KeyBindSender _keyBindSender;

        private readonly SavedVariablesParser _parser;

        public InMemoryActionBars(SavedVariablesParser parser, KeyBindSender keyBindSender, ActionsReader actionsReader)
        {
            _parser = parser;
            _keyBindSender = keyBindSender;
            _actionsReader = actionsReader;

            ActionBarItems = parser.GetByName("actionBars").Fields
                .ConvertAll(s => new ActionBarItem(s, keyBindSender, _actionsReader))
                .ToList();
        }

        private List<ActionBarItem> ActionBarItems { get; set; }

        public void Refresh()
        {
            ActionBarItems = _parser.GetByName("actionBars").Fields
                .ConvertAll(s => new ActionBarItem(s, _keyBindSender, _actionsReader))
                .ToList();
        }

        public void Dump()
        {
            Log.Logger.Debug("Action Bars: {@ActionBars}", ActionBarItems);
        }

        public ActionSlot GetActionSlot(int spellId)
        {
            return ActionBarItems.First(s => s.SpellId == spellId).ActionSlot;
        }

        public string GetSpellName(ActionSlot actionSlot)
        {
            return ActionBarItems.First(s => s.ActionSlot == actionSlot).SpellName;
        }

        public ActionBarItem GetActionBarItem(int spellId)
        {
            return ActionBarItems.Find(s => s.SpellId == spellId);
        }

        public bool Contains(int spellId)
        {
            return ActionBarItems.Any(s => s.SpellId == spellId);
        }
    }
}