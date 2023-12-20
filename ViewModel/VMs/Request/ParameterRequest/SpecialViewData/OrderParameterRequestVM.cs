using System.Collections.Generic;
using System.Collections.ObjectModel;

using ViewModel.Classes;

namespace ViewModel.VMs.Request.ParameterRequest.SpecialViewData
{
    public abstract class OrderParameterRequestVM : ParameterRequestVM
    {
        public abstract ObservableCollection<string> Columns { get; }
        
        public ObservableCollection<SortItem> Sort { get; set; } = new();

        public abstract string GetMainPartRequest();

        public sealed override string GetRequest()
        {
            var command = GetMainPartRequest();
            if (Sort.Count > 0)
            {
                List<string> sortStrings = new();
                foreach (var sortItem in Sort)
                {
                    sortStrings.Add($"{sortItem.ColumnName} {sortItem.SortMode}");
                }
                command += " ORDER BY " + string.Join(", ", sortStrings);
            }
            return command;
        }
    }
}
