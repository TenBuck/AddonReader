using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AddonReader.Data;

namespace AddonReader.Parser
{
    public class SavedVariableParser
    {
        private readonly string actionBarsPropertyName = "actionBars";
        private readonly string addonConfigPropertyName = "addonReader";

        private readonly BitmapProvider bitmapProvider;
        private readonly string framesPropertyName = "frames";

        private readonly string kbPropertyName = "kb";

        private readonly string profileName;

        private readonly string savedVariablePath =
            @"C:\Program Files (x86)\World of Warcraft\_classic_\WTF\Account\FLAGMIRLOL\SavedVariables\TheFrames.lua";


        public SavedVariableParser(string charName, string serverName, BitmapProvider bitmapProvider)
        {
            this.bitmapProvider = bitmapProvider;
            profileName = charName + " - " + serverName;
        }

        public AddonConfig AddonConfig { get; private set; }

        public void Load()
        {
            var lua = File.ReadAllText(savedVariablePath);

            var json = LuaParser.LuaTableToJson(lua);

            var options = new JsonDocumentOptions
            {
                AllowTrailingCommas = true
            };

            using (var document = JsonDocument.Parse(json, options))
            {
                var root = document.RootElement;
                var addonTableElement = root.GetProperty("FramesDB");

                var profilesElement = addonTableElement.GetProperty("profiles");


                if (profilesElement.TryGetProperty(profileName, out var profileElement))
                {
                    AddonConfig = ParseAddonConfig(profileElement.GetProperty(addonConfigPropertyName));
                    // var kb = ParseKeyBindings(profileElement.GetProperty(kbPropertyName));
                    // var actionBars = ParseActionBars(profileElement.GetProperty(actionBarsPropertyName));
                    var dataFrames = ParseDataFrames(profileElement.GetProperty(framesPropertyName));
                }
            }
        }

        private AddonConfig ParseAddonConfig(JsonElement addonConfigElement)
        {
            var addonConfigDict = new Dictionary<string, int>();
            foreach (var config in addonConfigElement.EnumerateObject())
                addonConfigDict.Add(config.Name, config.Value.GetInt32());

            return new AddonConfig(addonConfigDict);
        }


        private Dictionary<string, DataFrame> ParseDataFrames(JsonElement dataFramesElement)
        {
            var dataFramesDict = new Dictionary<string, DataFrame>();
            foreach (var dataFrameElement in dataFramesElement.EnumerateObject())
            {
                var data = dataFrameElement.Value.GetString().Split(";");

                var index = data[0];
                var name = data[1];

                dataFramesDict.Add(name, new DataFrame(int.Parse(index), name, bitmapProvider));
            }

            return dataFramesDict;
        }

        private Dictionary<string, int> ParseActionBars(JsonElement actionBarsElement)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> ParseKeyBindings(JsonElement keyBindingElement)
        {
            throw new NotImplementedException();
        }
    }
}