using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Classes;

namespace View.Implementations
{
    public class ConnectionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Connections", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ObservableCollection<Connection>),
            AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ObservableCollection<Connection> Connections
        {
            get => (ObservableCollection<Connection>)base["Connections"];
        }
    }
}
