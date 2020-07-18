using TenBot.AddonReader.Boxes;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.Readers
{
    public class ActionsReader
    {
        private readonly BoxMgr _boxMgr;

        public ActionsReader(BoxMgr boxMgr)
        {
            _boxMgr = boxMgr;
        }

        public SpellFailedCode FailedCode => (SpellFailedCode) _boxMgr.GetBoxByName("spellFailed-code").ToInt();

        public int LastFailedSpellId => _boxMgr.GetBoxByName("spellFailed-spellId").ToInt();

        public double GetSpellRemainingCooldown(ActionSlot actionSlot)
        {
            return _boxMgr.GetBoxByName("actionbars-Cooldown_" + (int) actionSlot).ToInt();
        }

        public bool IsSpellReady(ActionSlot actionSlot)
        {
            if (_boxMgr.GetBoxByName("actionbars-Cooldown_" + (int) actionSlot)
                .HasValue()) return false;

            return GetSpellRemainingCooldown(actionSlot) > 0;
        }

        public bool IsSpellInRange(ActionSlot actionSlot)
        {
            var range = _boxMgr.GetBoxByName("actionbars-IsInRange_" + (int) actionSlot)
                .ToBool();

            return range;
        }
    }
}