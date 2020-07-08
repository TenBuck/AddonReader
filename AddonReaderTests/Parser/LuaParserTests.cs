using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AddonReader.Parser.Tests
{
    [TestClass]
    public class LuaParserTests
    {
        [TestMethod]
        public void LuaTableToJsonTest()
        {
            var lua = File.ReadAllText("LuaTable.txt");

            var json = LuaParser.LuaTableToJson(lua);

            File.AppendAllText("JsonTable.json", json);
        }

        [TestMethod]
        public void LoadTest()
        {
            var path =
                @"C:\Program Files (x86)\World of Warcraft\_classic_\WTF\Account\FLAGMIRLOL\SavedVariables\TheFrames.lua";
            var lua = File.ReadAllText(path);

            var parser = new LuaParser(lua);

            parser = LuaParser.Parse(lua).Table("FramesDB").Field("profiles")
                .Field("Govbailout - Netherwind").Clone();

            var kb =
                parser
                    .Field("kb")
                    .GetList();
        }
    }
}