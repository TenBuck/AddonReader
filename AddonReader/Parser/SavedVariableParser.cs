using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using AddonReader.Data;

namespace AddonReader.Parser
{
    public class SavedVariableParser
    {
        private readonly string actionBarsPropertyName = "actionbars";
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


        public void LoadWith()
        {
            var config = LuaParser.Parse(File.ReadAllText(savedVariablePath))
                .Table("FramesDB")
                .Field("profiles")
                .Field("kb");

            
        }
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
                    var kb = ParseKeyBindings(profileElement.GetProperty(kbPropertyName));
                    var actionBars = ParseActionBars(profileElement.GetProperty(actionBarsPropertyName));
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


        private List<DataFrame> ParseDataFrames(JsonElement dataFramesElement)
        {
            var dataFrames = new List<DataFrame>();
            foreach (var dataFrame in dataFramesElement.EnumerateObject())
            {
                var dataFrameBuilder = new DataFramerBuilder(AddonConfig, bitmapProvider);

                dataFrames.Add(dataFrameBuilder.BuildFromParse(dataFrame.Value.GetString()));
            }

            return dataFrames;
        }

        private List<ActionBarItem> ParseActionBars(JsonElement actionBarsElement)
        {
            var actionBars = new List<ActionBarItem>();

            foreach (var actionBar in actionBarsElement.EnumerateObject())
            {
                var actionBarItem = ActionBarItem.Parse(actionBar.Value.GetString());
                actionBars.Add(actionBarItem);
            }

            return actionBars;
        }


        // Currently only saves the first keybinding
        private List<KeyBind> ParseKeyBindings(JsonElement keyBindingsElement)
        {
            var keyBindings = new List<KeyBind>();

            foreach (var keyBinding in keyBindingsElement.EnumerateObject())
            {
                var keyBind = KeyBind.ParseKeyBind(keyBinding.Value.GetString());

                if (keyBind != null) keyBindings.Add(keyBind);
            }

            return keyBindings;
        }
    }
}