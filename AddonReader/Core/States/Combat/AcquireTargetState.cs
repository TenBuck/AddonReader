using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.Core.WowPlayer;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class AcquireTargetState : IBotState
    {
        
        private readonly Stack<IBotState> _botStates;
        private readonly Player _player;
        private readonly KeyBindSender _keyBindSender;
        private readonly PlayerTarget _target;

        // private bool IsFirstUpdate = true;


        public AcquireTargetState(Stack<IBotState> botStates, Player player, KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _player = player;

            _target = player.Target;
            _keyBindSender = keyBindSender;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Acquire Target State");

            await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);


            if (_target.Reader.Exists && !_target.Reader.IsDead && !_target.Reader.IsTapped)
            {
                await _keyBindSender.SimulateKeyPress(KeyBinding.STOPAUTORUN);
                _botStates.Push(new MoveToTargetState(_botStates, _player, _keyBindSender));
                return;
            }

            await _keyBindSender.SimulateKeyPress(KeyBinding.TURNLEFT);
            await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);
        }
    }
}