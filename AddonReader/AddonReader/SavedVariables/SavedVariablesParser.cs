using System;
using System.Collections.Generic;
using System.IO;
using TenBot.AddonReader.SavedVariables.Parser;

namespace TenBot.AddonReader.SavedVariables
{
    public class SavedVariablesParser
    {
        private const string SavedVariablePath =
            @"C:\Program Files (x86)\World of Warcraft\_classic_\WTF\Account\FLAGMIRLOL\SavedVariables\TheFrames.lua";

        private readonly string _profileName;

        public SavedVariablesParser(string charName, string serverName)
        {
            _profileName = charName + " - " + serverName;

            var luaParser = LuaParser.Parse(File.ReadAllText(SavedVariablePath));

            SavedVariablesList = new List<SavedVariable>
            {
                new SavedVariable("FramesDB.profiles." + _profileName + ".addonConfig")
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + _profileName + ".frames")
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + _profileName + ".keybindings")
                    .SetValueFromLuaParser(luaParser),
                new SavedVariable("FramesDB.profiles." + _profileName + ".actionBars")
                    .SetValueFromLuaParser(luaParser)
            };
        }

        public List<SavedVariable> SavedVariablesList { get; }

        public SavedVariable GetByName(string name)
        {
            return SavedVariablesList.Find(s => s.Name == name) ??
                   throw new InvalidOperationException(name + "saved variable not found");
        }
    }
}