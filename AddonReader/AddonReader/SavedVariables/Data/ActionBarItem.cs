using System;
using System.Threading.Tasks;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables.Data
{
    public class ActionBarItem
    {
        private readonly KeyBinding _keyBinding;
        private readonly KeyBindSender _keyBindSender;

        public ActionBarItem(string item, KeyBindSender keyBindSender)
        {
            _keyBindSender = keyBindSender;
            var data = item.Split(';');

            ActionSlot = (ActionSlot) int.Parse(data[0]);

            SpellName = data[1];
            SpellId = int.Parse(data[2]);
            SpellCost = data.Length < 4 ? 0 : int.Parse(data[3]);
            _keyBinding = (KeyBinding) Enum.Parse(typeof(KeyBinding), ActionSlot.ToString(), true);
        }

        public int SpellCost { get; }

        public int SpellId { get; }

        public string SpellName { get; }

        public ActionSlot ActionSlot { get; }

        public async Task Send()
        {
            await _keyBindSender.SimulateKeyPress(_keyBinding);
        }
    }
}