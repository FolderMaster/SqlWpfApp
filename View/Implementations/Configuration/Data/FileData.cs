using System.Collections.Generic;

namespace View.Implementations.Configuration.Data
{
    public class FileData
    {
        public WindowData WindowData { get; set; }

        public IEnumerable<ConnectionData> ConnectionData { get; set; }

        public FileData() { }

        public FileData(WindowData windowData, IEnumerable<ConnectionData> connectionData)
        {
            WindowData = windowData;
            ConnectionData = connectionData;
        }
    }
}
