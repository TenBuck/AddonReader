using System.Runtime.InteropServices.ComTypes;

namespace AddonReader
{
    public interface IReader<T>
    {
        public T Value { get; }
    }
}