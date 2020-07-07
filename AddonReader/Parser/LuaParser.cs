using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace AddonReader.Parser
{
    public class LuaParser
    {
        private readonly JsonElement _root;

        private JsonElement currentJsonElement;

        private LuaParser(JsonElement root)
        {
            _root = root.Clone();
            currentJsonElement = _root;
        }

        public LuaParser(string lua)
        {
            var json = LuaTableToJson(lua);

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            var document = JsonDocument.Parse(json, options);
            _root = document.RootElement;
            currentJsonElement = _root;
        }

        public static string LuaTableToJson(string lua)
        {
            var sb = new StringBuilder();
            const string singleLineComment = @"--(.*?)\r?\n";


            sb.Append('{');
            sb.Append(lua);
            //sb.Append(Regex.Replace(lua, singleLineComment, me =>
            {
                //
                // if (me.Value.StartsWith("--"))
                //    {
                //      return Environment.NewLine;
                //}
                //return me.Value;

                //}, RegexOptions.Singleline));            
                sb.Append('}');
                sb.Replace("[", "");
                sb.Replace("]", "");
                sb.Replace("=", ":");
                sb.Replace("FramesDB", "\"FramesDB\"");
                sb.Replace("\\", "\\\\");


                return sb.ToString();
            }
        }

        public static LuaParser Parse(string lua)
        {
            return new LuaParser(lua);
        }


        public LuaParser Table(string tableName)
        {
            currentJsonElement = currentJsonElement.GetProperty(tableName);
            return this;
        }

        public LuaParser Field(string fieldName)
        {
            currentJsonElement = currentJsonElement.GetProperty(fieldName);
            return this;
        }

        public Dictionary<string, string> Get()
        {
            var dict = new Dictionary<string, string>();
            foreach (var element in currentJsonElement.EnumerateObject())
            {
                var value = element.Value.ValueKind == JsonValueKind.String
                    ? element.Value.GetString()
                    : element.Value.ToString();

                dict.Add(element.Name, value);
            }

            Reset();
            return dict;
        }

        private void Reset()
        {
            currentJsonElement = _root;
        }

        public LuaParser Clone()
        {
            return new LuaParser(currentJsonElement);
        }
    }
}