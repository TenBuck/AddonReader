using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Units;
using TenBot.Core.WowPlayer;

namespace TenBot.Core.States.Combat
{
    public class RestState : IBotState
    {
        private readonly Stack<IBotState> _botStates;
        private readonly Player _player;

        public RestState(Stack<IBotState> botStates, Player player)
        {
            _botStates = botStates;
            _player = player;
        }

        public async Task Update()
        {
            Log.Logger.Information("Rest State");


            if (!_player.Auras.HasBuff(168)) await _player.Auras.CastBuff(168);
            if (_player.Reader.HealthPercent > 50 && _player.Reader.PowerPercent > 50) _botStates.Pop();
        }
    }
}