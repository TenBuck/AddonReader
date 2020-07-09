using System.Drawing;

namespace TenBot.AddonReader.Frames
{
    public interface IDataFrame
    {
    }
}

namespace TenBot
{
    public interface IDataFrame
    {
        int Index { get; }

        string Name { get; }

        Point Point { get; set; }
    }
}