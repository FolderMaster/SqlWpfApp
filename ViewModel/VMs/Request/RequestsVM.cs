using CommunityToolkit.Mvvm.ComponentModel;

using System.Data;

using ViewModel.Interfaces;
using ViewModel.Services;

namespace ViewModel.VMs.Request
{
    public partial class RequestsVM : ObservableObject
    {
        [ObservableProperty]
        private DataTable executingResult;

        private IDbContextBuilder _dbContextCreator;

        protected MessengerService _messengerService;

        protected IResourceService _resourceService;

        public RequestsVM(IDbContextBuilder dbContextCreator, IResourceService resourceService,
            IMessageService messageService)
        {
            _resourceService = resourceService;
            _messengerService = new MessengerService(_resourceService, messageService);
            _dbContextCreator = dbContextCreator;
        }

        protected void ExecuteSqlCommand(string sqlCommand) =>
            _messengerService.ExecuteWithExceptionMessage(() =>
                ExecutingResult = _dbContextCreator.Result.ExecuteCommand(sqlCommand));
    }
}