using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.Unit;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class MoveToTargetState : IBotState
    {
       
        private readonly Stack<IBotState> _botStates;
        private readonly AddonReaderMgr _addonReaderMgr;
        private readonly ILogger _logger;
        private readonly PlayerReader _playerReader;
        private readonly KeyBindSender _keyBindSender;
        
        private TargetBase _target;

        public MoveToTargetState(ILogger logger, Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr, KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _playerReader = _addonReaderMgr.Player;
            _keyBindSender = keyBindSender;
           
            _target = _addonReaderMgr.Target;
            _logger = logger;
        }

        public async Task Update()
        {
            _logger.Information("MoveToTarget State: {IsInRange}",_playerReader.IsSpellInRange);
            if (_playerReader.IsSpellInRange)
            {
                await _keyBindSender.SimulateKeyPress(KeyBinding.ACTIONBUTTON1);
                _logger.Information("Cooldown remaining: {CD}", _addonReaderMgr.ActionsReader.GetSpellRemainingCooldown(133));
            }

            if (_target.Health == 0)
            {
                _botStates.Pop();
            }
            else
            {
                await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);
            }

         
            _logger.Debug("{@Player}", _playerReader);
        }
    }
}