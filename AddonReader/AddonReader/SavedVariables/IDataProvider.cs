namespace TenBot.AddonReader.SavedVariables
{
    public interface IDataProvider 
    {
        public void Refresh();
        public void Dump();
    }
}