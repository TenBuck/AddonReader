using System.Collections.Generic;

namespace TenBot.AddonReader.SavedVariables.Data
{
    public class AddonConfigItem 
    {
        public KeyValuePair<string, int> ConfigPair { get; }

        public AddonConfigItem(string value)
        {
            var data = value.Split(';');
            var pair = new KeyValuePair<string, int>(data[0], int.Parse(data[1]));
            ConfigPair = pair;
        }
        public static AddonConfigItem Parse(string s)
        {
            return new AddonConfigItem(s);
        }
    }
}