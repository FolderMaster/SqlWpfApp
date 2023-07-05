using System;

using ViewModel.Interfaces;

namespace ViewModel.Services
{
    public class MessengerService
    {
        private static string _errorTitleResourceName = "ErrorMessageTitle";

        private IResourceService _resourceService;

        private IMessageService _errorMessageService;

        public MessengerService(IResourceService resourceService,
            IMessageService errorMessageService)
        {
            _resourceService = resourceService;
            _errorMessageService = errorMessageService;
        }

        public bool ShowMessage(IMessageService messageService, string titleResourceKey,
            string descriptionResourceKey) =>
            messageService.ShowMessage(_resourceService.GetString(descriptionResourceKey),
                _resourceService.GetString(titleResourceKey));

        public void ExecuteWithExceptionMessage(Action action, Action? errorAction = null)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                _errorMessageService.ShowMessage(ex.Message,
                    _resourceService.GetString(_errorTitleResourceName));
                errorAction?.Invoke();
            }
        }
    }
}