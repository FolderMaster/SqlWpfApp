using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Classes
{
    public class Connection
    {
        public ObservableCollection<Authorization> Authorizations { get; set; } = new();

        public string Server { get; set; } = "";

        public string DataBase { get; set; } = "";
    }
}
