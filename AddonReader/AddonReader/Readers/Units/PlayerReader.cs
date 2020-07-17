using System.Threading.Tasks;
using TenBot.AddonReader.Boxes;
using TenBot.AddonReader.Readers.ActionBars;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.SavedVariables;
using TenBot.Extensions;

namespace TenBot.AddonReader.Readers.Units
{
    public class PlayerReader : UnitReader
    {
        private readonly InMemoryActionBars _actionBars;
        private readonly ActionsReader _actionsReader;
        private readonly AuraReader _auraReader;
        private readonly PositionReader _positionReader;


        public PlayerReader(BoxMgr boxMgr, AuraReader auraReader, ActionsReader actionsReader,
            InMemoryActionBars actionBars, PositionReader positionReader) : base(boxMgr, "player")
        {
            _auraReader = auraReader;
            _actionsReader = actionsReader;
            _actionBars = actionBars;
            _positionReader = positionReader;
        }

        public int Casting => _boxMgr.GetBoxByName(UnitName + "Cast").ToInt();
        public int Durability => _boxMgr.GetBoxByName(UnitName + "durability").ToInt();
        public int Freeslots => _boxMgr.GetBoxByName(UnitName + "freeslots").ToInt();


        public bool IsSpellReady(int spellId)
        {
            return _actionsReader.IsSpellReady(spellId);
        }

        public bool HasBuff(int spellId)
        {
            return _auraReader.Buffs.Contains(spellId);
        }

        public async Task CastBuff(int spellId)
        {
            var auraSpell = _actionBars.GetActionBarItem(spellId);
            if (auraSpell.SpellCost < Power) await auraSpell.Send();
        }
        public async Task CastSpell(int spellId)
        {
            if (CanCast(spellId)) await _actionBars.GetActionBarItem(spellId).Send();


            // if (Casting != spellId) Log.Logger.Information("Failed to cast");
        }

        public bool CanCast(int spellId)
        {
            return _actionBars.GetActionBarItem(spellId).SpellCost < Power && _actionsReader.IsSpellInRange(spellId);
        }
    }
}