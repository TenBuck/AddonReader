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
        private readonly Dictionary<string, List<Box>> _boxDictionary = new Dictionary<string, List<Box>>();
        private readonly SavedVariablesParser _savedVariablesParser;
        private List<Box> _boxes;
        private readonly ILogger _logger;

        public BoxMgr(SavedVariablesParser savedVariablesParser, BoxBuilder boxBuilder,
            ILogger logger)
        {
            _savedVariablesParser = savedVariablesParser;
            _logger = logger;

            _boxes = _savedVariablesParser.GetByName("frames").Fields.ConvertAll(boxBuilder.BuildFromParse)
                .OrderBy(s => s.Index)
                .ToList();


            _logger.Information("Loaded Boxes from Saved Variables..");
        }

        public bool IsAddonVisible(int errorInt)
        {
            if (GetBoxByName("ErrorStart").Color.ToInt() != errorInt &&
                GetBoxByName("ErrorEnd").Color.ToInt() != errorInt)
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
                        .FindAll(s => s.Name.Contains(name)));

            return _boxDictionary[name];
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Dump()
        {
            _logger.Information("Boxes : {@Boxes}", _boxes);
        }
    }
}