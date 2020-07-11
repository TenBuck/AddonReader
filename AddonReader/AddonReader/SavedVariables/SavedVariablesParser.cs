namespace TenBot.AddonReader.SavedVariables
{
    public class SavedVariablesParser
    {
        private readonly string _filePath;
        private readonly string _profileName;


        public SavedVariablesParser(string name, string server, string filePath) : this(name, server)
        {
            _filePath = filePath;
        }

        public SavedVariablesParser(string name, string server)
        {
            _profileName = name + " - " + server;
        }

        public SavedVariable GetByName(string name)
        {
            return new SavedVariable("FramesDB.profiles." + _profileName + "." + name);
        }

        public SavedVariable GetGlobalByName(string name)
        {
            return new SavedVariable("FramesDB.global." + name);
        }
    }
}