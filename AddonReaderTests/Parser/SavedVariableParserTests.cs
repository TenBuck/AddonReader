using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AddonReader.SavedVariables;

namespace AddonReader.Parser.Tests
{
    [TestClass()]
    public class SavedVariableParserTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            var lua = File.ReadAllText("LuaTable.txt");

            var json = LuaParser.LuaTableToJson(lua);


            var rect = new BitmapProvider(new Rectangle(10, 10, 10, 10));


            SavedVariablesParser parser = new SavedVariablesParser("Govbailout","Netherwind");
            parser.Load();
        }
    }
}