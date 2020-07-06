using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AddonReader.Parser.Tests
{
    [TestClass()]
    public class LuaParserTests
    {
        [TestMethod()]
        public void LuaTableToJsonTest()
        {
            var lua = File.ReadAllText("LuaTable.txt");

            var json = LuaParser.LuaTableToJson(lua);

            File.AppendAllText("JsonTable.json", json);
        }

        [TestMethod()]
        public void JsonToObjectTest()
        {
            var lua = File.ReadAllText("LuaTable.txt");

            var json = LuaParser.LuaTableToJson(lua);




        }
    }
}