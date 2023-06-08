using CommunityToolkit.Mvvm.ComponentModel;

namespace SQLiteWpfApp.ViewModels.VMs.Request
{
    public partial class ParametersVM : ObservableObject
    {
        [ObservableProperty]
        private object[] parameters;

        public object this[int index]
        {
            get => Parameters[index];
            set
            {
                if (Parameters[index].GetType() == value.GetType())
                {
                    Parameters[index] = value;
                }
                OnPropertyChanged($"[{index}]");
            }
        }

        public ParametersVM(object[] parameters)
        {
            Parameters = parameters;
        }
    }
}