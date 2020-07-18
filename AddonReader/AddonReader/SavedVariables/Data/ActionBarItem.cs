using System;
using System.Threading.Tasks;
using TenBot.AddonReader.Readers;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables.Data
{
    public class ActionBarItem
    {
        private readonly ActionsReader _actionsReader;
        private readonly KeyBinding _keyBinding;
        private readonly KeyBindSender _keyBindSender;

        public ActionBarItem(string item, KeyBindSender keyBindSender, ActionsReader actionsReader)
        {
            _keyBindSender = keyBindSender;
            _actionsReader = actionsReader;
            var data = item.Split(';');

            ActionSlot = (ActionSlot) int.Parse(data[0]);

            SpellName = data[1];
            SpellId = int.Parse(data[2]);
            SpellCost = data.Length < 4 ? 0 : int.Parse(data[3]);
            _keyBinding = (KeyBinding) Enum.Parse(typeof(KeyBinding), ActionSlot.ToString(), true);
        }

        public double CooldownRemaining => _actionsReader.GetSpellRemainingCooldown(ActionSlot);

        public bool IsSpellReady => _actionsReader.IsSpellReady(ActionSlot);

        public bool IsSpellInRange => _actionsReader.IsSpellInRange(ActionSlot);

        public int SpellCost { get; }

        public int SpellId { get; }

        public string SpellName { get; }

        public ActionSlot ActionSlot { get; }

        public async Task<SpellFailedCode> Send()
        {
            await _keyBindSender.SimulateKeyPress(_keyBinding);

            return _actionsReader.LastFailedSpellId == SpellId ? _actionsReader.FailedCode : SpellFailedCode.Success;
        }
    }
}