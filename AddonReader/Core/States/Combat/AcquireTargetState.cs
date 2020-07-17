using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Unit;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class AcquireTargetState : IBotState
    {
        private readonly AddonReaderMgr _addonReaderMgr;
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly TargetReader _targetReader;

        // private bool IsFirstUpdate = true;


        public AcquireTargetState(Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _targetReader = _addonReaderMgr.Target;
            _keyBindSender = keyBindSender;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Acquire Target State");

            await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);


            if (_targetReader.Exists && !_targetReader.IsDead && !_targetReader.IsTapped)
            {
                await _keyBindSender.SimulateKeyPress(KeyBinding.STOPAUTORUN);
                _botStates.Push(new MoveToTargetState(_botStates, _addonReaderMgr, _keyBindSender));
                return;
            }

            await _keyBindSender.SimulateKeyPress(KeyBinding.TURNLEFT);
            await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);
        }
    }
}