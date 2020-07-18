using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.Core.WowPlayer;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class FightState : IBotState
    {
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly Player _player;
        private readonly PlayerTarget _target;

        private SpellFailedCode _lastSpellFailedCode;

        public FightState(Stack<IBotState> botStates, Player player,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;

            _keyBindSender = keyBindSender;
            _player = player;
            _target = player.Target;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Fight State");


            if (_target.Reader.IsDead)
            {
                if (!_player.Reader.IsInCombat)
                {
                    _botStates.Pop();
                    _botStates.Push(new LootState(_botStates, _player, _keyBindSender));
                    await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETLASTTARGET);
                    return;
                }
            }

            

            if (_player.Reader.IsInCombat)
            {
                if ( _player.Target.Target.Name == _player.Reader.Name)
                    _lastSpellFailedCode =  await _player.Spells.CastSpell(133);
                else
                {
                    await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);
                    return;
                }
            }
            else
            {
                _lastSpellFailedCode = await _player.Spells.CastSpell(133);
            }

            switch (_lastSpellFailedCode)
            {
                case SpellFailedCode.Success:
                    return;
                case SpellFailedCode.OutOfRange:
                    await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);
                    return;
                //case SpellFailedCode.UnitNotInFront:


            }

            
        }
    }
}