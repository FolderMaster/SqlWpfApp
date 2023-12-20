using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace ViewModel.VMs.Request.ParameterRequest
{
    /// <summary>
    /// Класс представления модели для параметров.
    /// </summary>
    public abstract partial class ParameterRequestVM : ObservableObject
    {
        public virtual string Table => "";

        public abstract string GetRequest();

        public abstract Dictionary<string, object> GetParameters();
    }
}