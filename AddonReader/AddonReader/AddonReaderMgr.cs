using Serilog;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.ActionBars;
using TenBot.AddonReader.Readers.Unit;
using TenBot.AddonReader.Readers.Units;
using TenBot.AddonReader.SavedVariables;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        private readonly ILogger _logger;


        private readonly SavedVariablesParser _savedVariablesParser;
        public readonly AuraReader AuraReader;

        public AddonReaderMgr(SavedVariablesParser savedVariablesParser,
            ILogger logger, PlayerReader playerReader, TargetOfTarget targetOfTarget,
            PositionReader positionReader, TargetReader target, ActionsReader actions, AuraReader auraReader)

        {
            Player = playerReader;
            TargetOfTarget = targetOfTarget;
            PositionReader = positionReader;
            Target = target;
            _savedVariablesParser = savedVariablesParser;
            _logger = logger;
            AuraReader = auraReader;
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
            _logger.Debug("{@Actionsn}", ActionsReader);
            _logger.Debug("{@Aura}", AuraReader);
        }
    }
}