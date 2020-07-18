using System.Collections.Generic;
using System.Threading.Tasks;
using TenBot.Core.WowPlayer;

namespace TenBot.Core.States.Combat
{
    public class FaceTargetState : IBotState
    {
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly Player _player;
        private readonly PlayerTarget _target;

        public FaceTargetState(Stack<IBotState> botStates, Player player,
            KeyBindSender keyBindSender)
        {
            
        }
        public Task Update()
        {
            throw new System.NotImplementedException();
        }
    }
}