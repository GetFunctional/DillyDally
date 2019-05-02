namespace GF.DillyDally.Wpf.Client.Core
{
    public sealed class InitializationSettings
    {
        public InitializationSettings(string databaseName, bool enforceRecreationOfDatabase, bool updateDatabase)
        {
            this.DatabaseName = databaseName;
            this.EnforceRecreationOfDatabase = enforceRecreationOfDatabase;
            this.UpdateDatabase = updateDatabase;
        }

        public string DatabaseName { get; }
        public bool EnforceRecreationOfDatabase { get; }
        public bool UpdateDatabase { get; }
    }
}