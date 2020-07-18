using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.Core.WowPlayer;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class LootState : IBotState
    {
        

        private readonly Stack<IBotState> _botStates;
        private readonly Player _player;
        private readonly KeyBindSender _keyBindSender;
        private readonly PlayerTarget _target;


        public LootState(Stack<IBotState> botStates, Player player,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _player = player;
            _keyBindSender = keyBindSender;
            _target = player.Target;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Loot State");
            if (!_target.Reader.Exists)
                await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETLASTTARGET);

            await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);

            await Task.Delay(4000);
            
            _botStates.Pop();
            _botStates.Push(new RestState(_botStates, _player));
        }
    }
}