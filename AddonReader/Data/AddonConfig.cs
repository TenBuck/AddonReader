using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AddonReader.Data
{
    public class AddonConfig
    {
        public int MaxRow { get; private set; }
        public int MaxColumn { get; private set; }
        public int Cellsize { get; private set; }
        public int StringMaxChar { get; private set; }
        public int BoxCount { get; private set; }

        public AddonConfig(Dictionary<string, int> configsDictionary)
        {
            
            var properties = typeof(AddonConfig).GetProperties();
            foreach (KeyValuePair<string, int> kvp in configsDictionary)
            {
                var property = typeof(AddonConfig).GetProperty(kvp.Key.ToString());

                if (property != null)
                {
                    var value = Convert.ChangeType(kvp.Value, property.PropertyType);

                    property.SetValue(this,value);
                }

            }
        }
    }
}