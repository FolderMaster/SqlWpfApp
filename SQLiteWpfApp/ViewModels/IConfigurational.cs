namespace SQLiteWpfApp.ViewModels
{
    public interface IConfigurational
    {
        public string DataBasePath { get; set; }

        public void Save();

        public void Load();
    }
}