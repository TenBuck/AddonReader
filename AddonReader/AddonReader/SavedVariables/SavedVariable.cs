#region

using System.Collections.Generic;

using TenBot.AddonReader.SavedVariables.Parser;

#endregion

namespace TenBot.AddonReader.SavedVariables
{
    public class SavedVariable
    {
        private readonly List<string> _path = new List<string>();

        public SavedVariable(string path)
        {
            var fields = path.Split(".");
            foreach (var field in fields) _path.Add(field);

            Name = _path[fields.Length - 1];
        }

        public string Name { get; }

        public List<string> Fields { get; set; } = new List<string>();

        public SavedVariable SetValueFromLuaParser(LuaParser luaParser)
        {
            var parser = luaParser.Clone();
            foreach (var s in _path) parser = parser.Field(s);

            Fields = parser.GetList();
            return this;
        }
    }
}