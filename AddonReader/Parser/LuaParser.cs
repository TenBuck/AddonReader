using System.Text;

namespace AddonReader.Parser
{
    public class LuaParser
    {
        public static string LuaTableToJson(string lua)
        {
            var sb = new StringBuilder();
            var singleLineComment = @"--(.*?)\r?\n";


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
    }
}