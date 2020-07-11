using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using TenBot.AddonReader.Frames;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;
using TenBot.Extensions;
using TenBot.Game.WowTypes;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        private readonly ILogger _logger;
        private readonly SavedVariablesParser _savedVariablesParser;
        private readonly WowWindow _wowWindow;
        private List<ActionBarItem> _actionBarItems;
        private readonly AddonConfigProvider _configProvider;


        public AddonReaderMgr(AddonConfigProvider configProvider, SavedVariablesParser savedVariablesParser,
            WowWindow wowWindow, ILogger logger)

        {
            _configProvider = configProvider;
            _savedVariablesParser = savedVariablesParser;
            _wowWindow = wowWindow;
            _logger = logger;
        }


        public Dictionary<KeyBinding, KeyBind> KeysByBinding { get; private set; }

        public BoxMgr BoxMgr { get; set; }

        public void Initialize()
        {
            InitializeBoxMgr();


            KeysByBinding = _savedVariablesParser.GetByName("keybindings").Fields
                .ConvertAll(KeyBind.Parse)
                .ToDictionary(x => x.KeyBinding, x => x);
            _logger.Information("Loaded Key Bindings from Saved Variables..");
            _logger.Debug("{@KeysByBinding}", KeysByBinding);
            


            _actionBarItems = _savedVariablesParser.GetByName("actionBars").Fields
                .ConvertAll(ActionBarItem.Parse);
            _logger.Information("Loaded Action Bars from Saved Variables..");
            _logger.Debug("{@ActionBarItem}", _actionBarItems);
        }

        private void InitializeBoxMgr()
        {
            BoxMgr = new BoxMgr(_savedVariablesParser
                    .GetByName("frames"),
                _configProvider, _wowWindow
            );
            _logger.Information("Loaded frames from Saved Variables..");

            if (BoxMgr.GetBoxByName("ErrorStart").Color.ToInt() != _configProvider.ErrorInt &&
                BoxMgr.GetBoxByName("ErrorEnd").Color.ToInt() != _configProvider.ErrorInt)
                throw new Exception("could not locate addon frames...");
        }
    }
}