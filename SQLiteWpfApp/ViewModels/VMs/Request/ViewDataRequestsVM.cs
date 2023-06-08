using SQLiteWpfApp.ViewModels.Enums;
using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.ViewModels.VMs.Request
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