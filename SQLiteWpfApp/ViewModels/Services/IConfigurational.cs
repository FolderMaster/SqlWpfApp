namespace SQLiteWpfApp.ViewModels.Services
{
    public interface IConfigurational
    {
        public string DataBasePath { get; set; }

        public void Save();

        public void Load();
    }
}