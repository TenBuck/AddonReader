namespace TenBot.AddonReader.Readers
{
    public interface IReader<out T>
    {
        public T Value { get; }
    }
}