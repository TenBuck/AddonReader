using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Unit;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class LootState : IBotState
    {
        private readonly AddonReaderMgr _addonReaderMgr;

        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly TargetReader _target;


        public LootState(Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _keyBindSender = keyBindSender;


            _target = addonReaderMgr.Target;
        }

        public async Task Update()
        {
            Log.Logger.Information("Updating Loot State");
            if (!_target.Exists)
                await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETLASTTARGET);

            await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);

            await Task.Delay(4000);
            
            _botStates.Pop();
            _botStates.Push(new RestState(_botStates, _addonReaderMgr));
        }
    }
}