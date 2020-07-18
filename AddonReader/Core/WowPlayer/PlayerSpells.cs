using System.Threading.Tasks;
using TenBot.AddonReader.Readers.Units;
using TenBot.AddonReader.SavedVariables;
using TenBot.Game.WowTypes;

namespace TenBot.Core.WowPlayer
{
    public class PlayerSpells
    {
        private readonly InMemoryActionBars _actionBars;
        private readonly PlayerReader _playerReader;

        public PlayerSpells(PlayerReader playerReader, InMemoryActionBars actionBars)
        {
            _playerReader = playerReader;
            _actionBars = actionBars;
        }

        public async Task<SpellFailedCode> CastSpell(int spellId)
        {
            return await _actionBars.GetActionBarItem(spellId).Send();
        }

        public bool CanCast(int spellId)
        {
            return _actionBars.GetActionBarItem(spellId).SpellCost < _playerReader.Power &&
                   _actionBars.GetActionBarItem(spellId).IsSpellInRange;
        }
    }
}