using System.Threading.Tasks;
using GregsStack.InputSimulatorStandard;
using Serilog;
using TenBot.AddonReader.Readers.Units;

namespace TenBot.Core.WowPlayer
{
    public class Player
    {
        private readonly MouseServices _mouseServices;

        public Player(PlayerAuras playerAuras, PlayerSpells playerSpells, PlayerReader playerReader,
            PlayerTarget target, MouseServices mouseServices)
        {
            _mouseServices = mouseServices;
            Auras = playerAuras;
            Spells = playerSpells;
            Reader = playerReader;
            Target = target;
        }

        public PlayerTarget Target { get; }
        public PlayerAuras Auras { get; }
        public PlayerSpells Spells { get; }
        public PlayerReader Reader { get; }

        public async Task Rotate(int degrees)
        {
            
                var posStart = Reader.Position.Facing;
                await _mouseServices.RightClickMiddle(3.1416);
                var postDelta = posStart - Reader.Position.Facing;
                Log.Logger.Information("Moved: {Facing} with i={i}", postDelta );
                
            


           
        }

        public void Dump()
        {
            var props = GetType().GetProperties();

            foreach (var prop in props)
            {
                var value = prop.GetValue(this, null); // against prop.Name
                var name = prop.Name;
                Log.Logger.Information("{Name}: {@value}", name, value);
            }

            Log.Logger.Information("");
        }
    }
}