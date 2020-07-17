using System.Collections.Generic;
using GregsStack.InputSimulatorStandard.Native;
using Serilog;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader.SavedVariables
{
    public class InMemoryKeyBinds : IDataProvider
    {
        private readonly ILogger _logger;
        private readonly Dictionary<KeyBinding, KeyBind> _keyBinds;

        public InMemoryKeyBinds(SavedVariablesParser parser, ILogger logger)
        {
            _logger = logger;
            // TODO: better initializationc
            _keyBinds = parser.GetByName("keybindings").ParseKeyBind();
            
        }

        public List<VirtualKeyCode> GetKeyBind(KeyBinding keyBinding)
        {
            return _keyBinds.ContainsKey(keyBinding) ? _keyBinds[keyBinding].KeyList : null;
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }

        public void Dump()
        {
            _logger.Debug("Action Bars: {@KeyBindings}", _keyBinds);
        }
    }
}