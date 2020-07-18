using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.Core.WowPlayer;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class MoveToTargetState : IBotState
    {
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly Player _player;
        private readonly PlayerTarget _target;

        public MoveToTargetState(Stack<IBotState> botStates, Player player,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _player = player;


            _keyBindSender = keyBindSender;
            _target = _player.Target;
        }

        public async Task Update()
        {
            Log.Logger.Information("MoveToTarget State: ");
            if (_player.Spells.CanCast(133))
            {
                _botStates.Pop();
                _botStates.Push(new FightState(_botStates, _player, _keyBindSender));
                return;
            }

            if (!_target.Reader.Exists || _target.Reader.IsDead)
                _botStates.Pop();
            else
                await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);
        }
    }
}