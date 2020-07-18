using Serilog;
using TenBot.AddonReader.Readers;
using TenBot.AddonReader.Readers.Units;
using TenBot.Core.WowPlayer;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        
        private readonly ILogger _logger = Log.Logger;


        public AddonReaderMgr(Player player )
        {
            Player = player;
           
        }

      
        public Player Player { get; }

        public void Dump()
        {
            _logger.Debug("{@Player}", Player);
            _logger.Debug("{@Target}", Player.Target);
            _logger.Debug("{@TargetOfTarget}", Player.Target.Target);
            _logger.Debug("{@Position}", Player.Reader.Position);
        }
    }
}