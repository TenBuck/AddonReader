#region

using System.Collections.Generic;
using System.IO;
using TenBot.AddonReader.SavedVariables.Parser;

#endregion

namespace TenBot.AddonReader.SavedVariables
{
    public class SavedVariable
    {
        private readonly string _filePath =
            @"C:\Program Files (x86)\World of Warcraft\_classic_\WTF\Account\FLAGMIRLOL\SavedVariables\TheFrames.lua";

        private readonly List<string> _path = new List<string>();


        public SavedVariable(string path, string filePath) : this(path)
        {
            _filePath = filePath;
        }

        public SavedVariable(string path)
        {
            var fields = path.Split(".");
            foreach (var field in fields) _path.Add(field);

            Name = _path[fields.Length - 1];

            var parser = LuaParser.Parse(File.ReadAllText(_filePath));
            foreach (var s in _path) parser = parser.Field(s);


            Fields = parser.GetList();
        }


        public string Name { get; }

        public List<string> Fields { get; set; } = new List<string>();
    }
}