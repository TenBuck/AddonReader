using System;
using System.Collections.Generic;

using Serilog;

using TenBot.AddonReader.Frames;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Game.WowEntities;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        
        private readonly AddonConfig _addonConfig;

        public readonly List<DataFrame> _frames;
        private readonly List<ActionBarItem> _actionBarItems;
        private readonly List<KeyBind> _keyBinds;

        private readonly BitmapProvider _bitmapProvider;

        public AddonReaderMgr(BitmapProvider bitmapProvider)
        {
            _bitmapProvider = bitmapProvider;

            var savedVariables = new SavedVariablesParser("Govbailout", "Netherwind");

            _addonConfig = new AddonConfig(savedVariables.GetSavedVariableByName("addonConfig").Fields);

            bitmapProvider.AddonRectangle = _addonConfig.AddonRectangle;

            var framesBuilder = new DataFramerBuilder(_addonConfig, _bitmapProvider);
            _frames = savedVariables.GetSavedVariableByName("frames").Fields.ConvertAll(framesBuilder.BuildFromParse);

            _keyBinds = savedVariables.GetSavedVariableByName("keybindings").Fields
                .ConvertAll(KeyBind.Parse);

            _actionBarItems = savedVariables.GetSavedVariableByName("actionBars").Fields
                .ConvertAll(ActionBarItem.Parse);


            

        }


        
    }
}