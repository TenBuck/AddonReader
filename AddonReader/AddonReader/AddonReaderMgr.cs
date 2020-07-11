using System;
using System.Collections.Generic;
using Serilog;
using TenBot.AddonReader.Frames;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Extensions;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        private readonly ILogger _logger;
        private readonly SavedVariablesParser _savedVariablesParser;
        private readonly WowWindow _wowWindow;
        private List<ActionBarItem> _actionBarItems;

        private AddonConfig _addonConfig;
        private List<KeyBind> _keyBinds;


        public AddonReaderMgr(SavedVariablesParser savedVariablesParser, WowWindow wowWindow, ILogger logger)

        {
            _savedVariablesParser = savedVariablesParser;
            _wowWindow = wowWindow;
            _logger = logger;
        }

        public BoxMgr BoxMgr { get; set; }

        public void Initialize()
        {
            _addonConfig = new AddonConfig(_savedVariablesParser.GetByName("addonConfig").Fields);

            InitializeBoxMgr();


            _keyBinds = _savedVariablesParser.GetByName("keybindings").Fields
                .ConvertAll(KeyBind.Parse);
            _logger.Information("Loaded Key Bindings from Saved Variables..");

           
            _actionBarItems = _savedVariablesParser.GetByName("actionBars").Fields
                .ConvertAll(ActionBarItem.Parse);
            _logger.Information("Loaded Action Bars from Saved Variables..");
        }

        private void InitializeBoxMgr()
        {
            BoxMgr = new BoxMgr(_savedVariablesParser
                    .GetByName("frames"),
                _addonConfig, _wowWindow
            );
            _logger.Information("Loaded frames from Saved Variables..");

            if (BoxMgr.GetBoxByName("ErrorStart").Color.ToInt() != _addonConfig.ErrorInt &&
                BoxMgr.GetBoxByName("ErrorEnd").Color.ToInt() != _addonConfig.ErrorInt)
                throw new Exception("could not locate addon frames...");
        }
    }
}