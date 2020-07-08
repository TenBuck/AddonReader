using System;
using System.Collections.Generic;
using AddonReader.Parser;

namespace AddonReader.SavedVariables
{
    public class SavedVariable
    {
        private readonly List<string> _path = new List<string>();
        private Converter<string, dynamic> _converter;

        private List<string> _values = new List<string>();

        public SavedVariable(string path)
        {
            var fields = path.Split(".");
            foreach (var field in fields) _path.Add(field);
            Name = _path[^1];
        }
        public string Name { get; }

        public List<dynamic> Values => _values.ConvertAll(_converter ?? (s => s.ToString()));

        public SavedVariable AddConverter(Converter<string, dynamic> converter)
        {
            _converter = converter;
            return this;
        }

        public SavedVariable SetValueFromLuaParser(LuaParser luaParser)
        {
            var parser = luaParser.Clone();
            foreach (var s in _path) parser = parser.Field(s);

            _values = parser.GetList();
            return this;
        }
    }
}