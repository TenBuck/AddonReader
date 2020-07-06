using System.Collections.Generic;

namespace AddonReader.Data
{
    public class Profile
    {
        public string CharacterName { get; set; }
        public string Server { get; private set; }
        public Dictionary<int, string> KeyBindings { get; set; }

        public Dictionary<string, DataFrame> DataFrames { get; set; }

        public Dictionary<string, int> AddonReaderConfig { get; set; }

        public Dictionary<string, int> ActionBars { get; set; }
    }
}