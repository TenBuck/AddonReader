using Serilog;
using TenBot.AddonReader.Boxes;
using TenBot.AddonReader.SavedVariables;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.ActionBars
{
    public class ActionsReader
    {
        private readonly InMemoryActionBars _actionBars;

        private readonly BoxMgr _boxMgr;


        public ActionsReader(BoxMgr boxMgr, InMemoryActionBars actionBars)
        {
            _boxMgr = boxMgr;
            _actionBars = actionBars;
        }


        public double GetSpellRemainingCooldown(int spellId)
        {
            var actionSlot = _actionBars.GetActionSlot(spellId);
            return _boxMgr.GetBoxByName("actionbars-Cooldown_" + (int) actionSlot).ToInt();
        }

        public bool IsSpellReady(int spellId)
        {
            if (_boxMgr.GetBoxByName("actionbars-Cooldown_" + (int) _actionBars.GetActionSlot(spellId))
                .HasValue()) return false;

            return GetSpellRemainingCooldown(spellId) > 0;
        }

        public bool IsSpellInRange(int spellId)
        {
            var range = _boxMgr.GetBoxByName("actionbars-IsInRange_" + (int) _actionBars.GetActionSlot(spellId))
                .ToBool();
            Log.Logger.Information("In Range: {SpellID}: {InRange}", spellId, range);
            return range;
        }
    }
}