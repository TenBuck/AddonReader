using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TenBot.AddonReader.SavedVariables;

namespace TenBot.AddonReader.Frames
{
    public class BoxMgr
    {
        private readonly Dictionary<string, List<Box>> _boxDictionary = new Dictionary<string, List<Box>>();
        private readonly List<Box> _boxes;

        public BoxMgr(SavedVariable boxes, AddonConfig config, WowWindow wowWindow)
        {
            wowWindow.MoveWindow(Point.Empty);
            var bitmapProvider = new BitmapProvider(wowWindow.ClientToScreen(config.AddonRectangle));
            var framesBuilder = new BoxBuilder(config, bitmapProvider);

            _boxes = boxes.Fields.ConvertAll(framesBuilder.BuildFromParse).OrderBy(s => s.Index).ToList();
        }

        public void Reset()
        {
            _boxDictionary.Clear();
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
    }
}