﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using TenBot.AddonReader;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.Readers.Units;
using TenBot.Game.WowTypes;

namespace TenBot.Core.States.Combat
{
    public class MoveToTargetState : IBotState
    {
        private readonly AddonReaderMgr _addonReaderMgr;

        private readonly Stack<IBotState> _botStates;
        private readonly KeyBindSender _keyBindSender;
      private readonly PlayerReader _playerReader;

        private readonly TargetBase _target;

        public MoveToTargetState(Stack<IBotState> botStates, AddonReaderMgr addonReaderMgr,
            KeyBindSender keyBindSender)
        {
            _botStates = botStates;
            _addonReaderMgr = addonReaderMgr;
            _playerReader = _addonReaderMgr.Player;
            _keyBindSender = keyBindSender;
            _target = _addonReaderMgr.Target;
          
        }

        public async Task Update()
        {
            Log.Logger.Information("MoveToTarget State: ");
            if (_playerReader.CanCast(133))
            {
                _botStates.Pop();
                _botStates.Push(new FightState(_botStates, _addonReaderMgr, _keyBindSender));
                return;
            }

            if (!_target.Exists || _target.IsDead)
                _botStates.Pop();
            else
                await _keyBindSender.SimulateKeyPress(KeyBinding.INTERACTTARGET);
        }
    }
}