using System;

namespace View.Implementations.Configuration.Data
{
    public class ConnectionData
    {
        public Type ConnectionType { get; set; }

        public object Data { get; set; }

        public ConnectionData() { }

        public ConnectionData(Type connectionType, object data)
        {
            ConnectionType = connectionType;
            Data = data;
        }
    }
}
