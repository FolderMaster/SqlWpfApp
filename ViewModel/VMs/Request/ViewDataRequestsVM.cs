using ViewModel.Enums;
using ViewModel.Services;

namespace ViewModel.VMs.Request
{
    public class ViewDataRequestsVM : ViewRequestsVM
    {
        private TableName _tableName;

        public TableName TableName
        {
            get => _tableName;
            set
            {
                if (SetProperty(ref _tableName, value))
                {
                    ExecuteSqlCommand(CreateSelectCommand("*", $"{TableName}"));
                }
            }
        }

        public ViewDataRequestsVM(IMessageService messageService) : base(messageService) { }
    }
}