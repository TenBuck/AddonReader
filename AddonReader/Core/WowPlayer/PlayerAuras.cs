using System.Threading.Tasks;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.Units;
using TenBot.AddonReader.SavedVariables;

namespace TenBot.Core.WowPlayer
{
    public class PlayerAuras
    {
        private readonly InMemoryActionBars _actionBars;
        private readonly AuraReader _auraReader;
        private readonly PlayerReader _playerReader;

        public PlayerAuras(PlayerReader playerReader, AuraReader auraReader, InMemoryActionBars actionBars)
        {
            _playerReader = playerReader;
            _auraReader = auraReader;
            _actionBars = actionBars;
        }

        public bool HasBuff(int spellId)
        {
            return _auraReader.Buffs.Contains(spellId);
        }

        public async Task<bool> CastBuff(int spellId)
        {
            var auraSpell = _actionBars.GetActionBarItem(spellId);
            if (auraSpell.SpellCost < _playerReader.Power) await auraSpell.Send();

            return HasBuff(spellId);
        }
    }
}