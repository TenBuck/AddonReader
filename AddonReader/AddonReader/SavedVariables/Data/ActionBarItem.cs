using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables.Data 
{
    public class ActionBarItem 
    {
        public int SpellId { get; }

        public string SpellName { get; }

        public ActionSlot ActionSlot { get; }

        public ActionBarItem(ActionSlot actionSlot, int spellId, string spellName)
        {
            ActionSlot = actionSlot;
            SpellId = spellId;
            SpellName = spellName;
        }

        public ActionBarItem(string item)
        {
            var data = item.Split(';');

            ActionSlot = (ActionSlot)int.Parse(data[0]);

            SpellName = data[1];
            SpellId = int.Parse(data[2]);
        }

        public static ActionBarItem Parse(string item)
        {
            return new ActionBarItem(item);
        }
    }
}