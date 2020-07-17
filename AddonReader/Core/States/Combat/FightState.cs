using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.Readers.Units;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class FightState : IBotState
    {
        private readonly AddonReaderMgr _addonReaderMgr;
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly PlayerReader _player;
        private readonly TargetReader _target;

        public FightState(Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _keyBindSender = keyBindSender;
            _player = addonReaderMgr.Player;
            _target = addonReaderMgr.Target;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Fight State");
            if (_target.IsDead)
            {
                _botStates.Pop();
                _botStates.Push(new LootState(_botStates, _addonReaderMgr, _keyBindSender));
                return;
            }

            if (_target.Exists && _target.Health != 0)
                await _player.CastSpell(133);
            else
                await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETLASTTARGET);
        }
    }
}