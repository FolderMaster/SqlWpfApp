namespace ViewModel.Interfaces
{
    public interface IConfigurational
    {
        public string DataBaseConnectionString { get; set; }

        public void Save();

        public void Load();
    }
}