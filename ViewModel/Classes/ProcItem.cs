using CommunityToolkit.Mvvm.Input;
using System;

using ViewModel.Interfaces.Proces;

namespace ViewModel.Classes
{
    public class ProcData : IProcData
    {
        public IRelayCommand Command { get; private set; }

        public object Metadata { get; private set; }

        public ProcData(Action invoke, object metadata, Func<bool>? canExecute = null)
        {
            Command = canExecute != null ? new RelayCommand(invoke, canExecute) :
                new RelayCommand(invoke);
            Metadata = metadata;
        }
    }
}
