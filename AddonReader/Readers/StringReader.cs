using System.Collections.Generic;
using AddonReader.Extensions;

namespace AddonReader
{
    public class StringDataFrame : IReader<string>
    {
        private List<DataFrame> dataFrames = new List<DataFrame>();
        public StringDataFrame()
        {
            
        }

        public void AddDataFrame(DataFrame dataFrame)
        {
            dataFrames.Add(dataFrame);
        }

        public string Value
        {
            get
            {
                var value = "";
                foreach (var frame in dataFrames)
                {
                    value += frame.Color.ToChars();
                }

                return value;

            }
        }
    }
}