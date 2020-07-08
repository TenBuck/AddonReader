using System.Collections.Generic;
using System.IO;
using AddonReader.Data;
using AddonReader.Parser;
using AddonReader.SavedVariables.Data;

namespace AddonReader.SavedVariables
{
    public class SavedVariablesParser
    {
        private const string savedVariablePath =
            @"C:\Program Files (x86)\World of Warcraft\_classic_\WTF\Account\FLAGMIRLOL\SavedVariables\TheFrames.lua";

        private readonly string profileName;

        public List<SavedVariable> SavedVariables { get; private set; }

        public SavedVariablesParser(string charName, string serverName)
        {
            profileName = charName + " - " + serverName;
        }

        public SavedVariablesParser Load()
        {
            var luaParser = LuaParser.Parse(File.ReadAllText(savedVariablePath));

            SavedVariables = new List<SavedVariable>
            {
                new SavedVariable("FramesDB.profiles." + profileName + ".addonConfig")
                    .AddConverter(AddonConfigItem.Parse)
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + profileName + ".frames")
                    .AddConverter(DataFrame.Parse)
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + profileName + ".keybindings")
                    .AddConverter(KeyBind.Parse)
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + profileName + ".actionBars")
                    .AddConverter(ActionBarItem.Parse)
                    .SetValueFromLuaParser(luaParser)
            };
            return this;
        }

         


    }
}