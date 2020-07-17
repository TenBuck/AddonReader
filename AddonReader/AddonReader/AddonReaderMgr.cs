﻿using Serilog;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.ActionBars;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.Readers.Units;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        private readonly ILogger _logger;


        public AddonReaderMgr(
            ILogger logger, PlayerReader playerReader, TargetOfTarget targetOfTarget,
            PositionReader positionReader, TargetReader target, ActionsReader actions)

        {
            Player = playerReader;
            TargetOfTarget = targetOfTarget;
            PositionReader = positionReader;
            Target = target;

            _logger = logger;
            ActionsReader = actions;
        }

        public ActionsReader ActionsReader { get; }
        public PositionReader PositionReader { get; }
        public PlayerReader Player { get; }
        public TargetOfTarget TargetOfTarget { get; }
        public TargetReader Target { get; }


        public void Dump()
        {
            _logger.Debug("{@Player}", Player);
            _logger.Debug("{@Target}", Target);
            _logger.Debug("{@TargetOfTarget}", TargetOfTarget);
            _logger.Debug("{@Position}", PositionReader);
            _logger.Debug("{@Actions}", ActionsReader);
           
        }
    }
}