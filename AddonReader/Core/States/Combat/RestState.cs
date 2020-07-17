using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Units;

namespace TenBot.Core.States.Combat
{
    public class RestState : IBotState
    {
        private readonly Stack<IBotState> _botStates;
        private readonly PlayerReader _player;

        public RestState(Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr)
        {
            _botStates = botStates;
            _player = addonReaderMgr.Player;
        }

        public async Task Update()
        {
            Log.Logger.Information("Rest State");


            if (!_player.HasBuff(168)) await _player.CastBuff(168);
            if (_player.HealthPercent > 50 && _player.PowerPercent > 50) _botStates.Pop();
        }
    }
}