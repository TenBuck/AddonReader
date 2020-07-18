using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using TenBot.AddonReader.SavedVariables;
using TenBot.Extensions;

namespace TenBot.AddonReader.Boxes
{
    public class BoxMgr : IDataProvider
    {
        private readonly BoxBuilder _boxBuilder;
        private readonly ILogger _logger = Log.Logger;
        private readonly SavedVariablesParser _savedVariablesParser;
        private Dictionary<string, List<Box>> _boxDictionary = new Dictionary<string, List<Box>>();
        private List<Box> _boxes;

        public BoxMgr(SavedVariablesParser savedVariablesParser, BoxBuilder boxBuilder)
        {
            _savedVariablesParser = savedVariablesParser;
            _boxBuilder = boxBuilder;

            _boxes = _savedVariablesParser.GetByName("frames").Fields.ConvertAll(_boxBuilder.BuildFromParse)
                .OrderBy(s => s.Index)
                .ToList();
        }

        public void Refresh()
        {
            _boxes = _savedVariablesParser.GetByName("frames").Fields.ConvertAll(_boxBuilder.BuildFromParse)
                .OrderBy(s => s.Index)
                .ToList();

            _boxDictionary = new Dictionary<string, List<Box>>();
            _logger.Information("Loaded Boxes from Saved Variables..");
        }

        public void Dump()
        {
            _logger.Information("Boxes : {@Boxes}", _boxes);
        }

        public bool IsAddonVisible()
        {
            if (GetBoxByName("ErrorStart").Color.ToInt() != _boxBuilder.ErrorInt &&
                GetBoxByName("ErrorEnd").Color.ToInt() != _boxBuilder.ErrorInt)
                throw new Exception("could not locate addon frames...");
            return true;
        }

        public Box GetBoxByName(string name)
        {
            if (!_boxDictionary.ContainsKey(name))
                _boxDictionary
                    .Add(name, _boxes
                        .FindAll(s => s.Name == name));

            return _boxDictionary[name].First();
        }

        public List<Box> GetBoxListByName(string name)
        {
            if (!_boxDictionary.ContainsKey(name))
                _boxDictionary
                    .Add(name, _boxes
                        .FindAll(s => s.Name.StartsWith(name)));

            return _boxDictionary[name];
        }
    }
}