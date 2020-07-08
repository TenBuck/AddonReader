using System.Runtime.InteropServices.ComTypes;

namespace AddonReader
{
    public interface IReader<out T>
    {
        public T Value { get; }
    }
}