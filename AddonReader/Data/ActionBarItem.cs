using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AddonReader.WowTypes;

namespace AddonReader.Data
{
    public class ActionBarItem
    {
        public int SpellId { get; }

        public string SpellName { get; }

        public ActionSlot ActionSlot { get; }
        
        public ActionBarItem(ActionSlot actionSlot, int spellId, string spellName )
        {
            ActionSlot = actionSlot;
            SpellId = spellId;
            SpellName = spellName;
        }

        public static ActionBarItem Parse(string item)
        {
            var data = item.Split(';');

            var actionSlot = (ActionSlot)int.Parse(data[0]);
           
            var spellName = data[1];
            var spellId = int.Parse(data[2]);

            return new ActionBarItem(actionSlot, spellId, spellName);
        }
    }
}
