using System;

using View.Implementations.ResourceService;

using ViewModel.Interfaces;
using ViewModel.Interfaces.Services;

namespace View.Implementations.Proces.Windows
{
    public abstract class DbWindowProc : WindowProc
    {
        

        protected DbWindowProc(string name, ISession session,
            IWindowResourceService windowResourceService, IMessageService messageService) :
            base(name, session, windowResourceService, messageService) =>
            session.DbContextChanged += DbContextBuilder_ResultConnectionCreated;

        private void DbContextBuilder_ResultConnectionCreated(object? sender, EventArgs e)
        {
            Abort();
            Data.Command.NotifyCanExecuteChanged();
        } 

        protected override bool CanExecute() =>
            _session.DbContext != null && _session.DbContext.CanConnect;
    }
}