using System.Collections.ObjectModel;

namespace ViewModel.Classes.Connections.MsSqlServer
{
    public class MsSqlServerConnectionDataSet
    {
        public MsSqlServerConnectionData Connection { get; set; } = new();

        public ObservableCollection<MsSqlServerCredentialData> Credentials { get; set; } = [];
    }
}
