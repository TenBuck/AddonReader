using System.Collections.Generic;
using TenBot.AddonReader.Frames;
using TenBot.AddonReader.SavedVariables;
using TenBot.AddonReader.SavedVariables.Data;

namespace TenBot.AddonReader
{
    public class AddonReaderMgr
    {
        private readonly SavedVariablesParser _savedVariablesParser;
        private readonly WowWindow _wowWindow;
        private List<ActionBarItem> _actionBarItems;

        private AddonConfig _addonConfig;
        private List<KeyBind> _keyBinds;


        public AddonReaderMgr(SavedVariablesParser savedVariablesParser, WowWindow wowWindow)

        {
            _savedVariablesParser = savedVariablesParser;
            _wowWindow = wowWindow;
        }

        public BoxMgr BoxMgr { get; set; }

        public void Initialize()
        {
            _addonConfig = new AddonConfig(_savedVariablesParser.GetByName("addonConfig").Fields);

            var savedVariables = _savedVariablesParser.SavedVariablesList;

            BoxMgr = new BoxMgr(_savedVariablesParser
                    .GetByName("frames"),
                _addonConfig, _wowWindow
            );


            _keyBinds = _savedVariablesParser.GetByName("keybindings").Fields
                .ConvertAll(KeyBind.Parse);

            _actionBarItems = _savedVariablesParser.GetByName("actionBars").Fields
                .ConvertAll(ActionBarItem.Parse);
        }
    }
}