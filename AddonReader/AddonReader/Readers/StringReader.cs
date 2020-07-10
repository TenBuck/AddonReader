using System.Collections.Generic;

using TenBot.Extensions;

namespace TenBot.AddonReader.Readers
{
    public class StringDataFrame : IReader<string>
    {
        private readonly List<DataFrame> _dataFrames = new List<DataFrame>();
        public string Value
        {
            get
            {
                var value = "";
                foreach (var frame in _dataFrames) value += frame.Color.ToChars();

                return value;
            }
        }
        public void AddDataFrame(DataFrame dataFrame)
        {
            _dataFrames.Add(dataFrame);
        }
    }
}