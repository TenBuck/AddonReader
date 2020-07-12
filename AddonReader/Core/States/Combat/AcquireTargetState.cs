﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.Readers.Units;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class AcquireTargetState : IBotState
    {
        private readonly AddonReaderMgr _addonReaderMgr;
        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
        private readonly ILogger _logger;
        private readonly PlayerReader _player;
        private readonly TargetBase _targetReader;


        public AcquireTargetState(ILogger logger, Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr,
            KeyBindSender keyBindSender)
        {
            _logger = logger;
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _player = _addonReaderMgr.Player;
            _targetReader = _addonReaderMgr.Target;
            _keyBindSender = keyBindSender;
        }

        public async Task Update()
        {
            await _keyBindSender.SimulateKeyPress(KeyBinding.TARGETNEARESTENEMY);
            if (_targetReader.Level > 0 && _targetReader.Level < 60)
                _botStates.Push(new MoveToTargetState(_logger, _botStates, _addonReaderMgr, _keyBindSender));
        }
    }
}