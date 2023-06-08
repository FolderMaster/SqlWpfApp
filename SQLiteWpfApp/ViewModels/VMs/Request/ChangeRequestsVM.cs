using SQLiteWpfApp.ViewModels.Enums;
using SQLiteWpfApp.ViewModels.Services;

namespace SQLiteWpfApp.ViewModels.VMs.Request
{
    public class ChangeRequestsVM : ViewRequestsVM
    {
        protected string CreateInsertCommand(string table, string columns, string values) =>
            $"INSERT INTO {table} ({columns}) VALUES ({values})";

        protected string CreateUpdateCommand(string table, string updating,
            string condition = "") =>
            $"UPDATE {table} SET {updating}" +
            (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");

        protected string CreateDeleteCommand(string table, string condition) =>
            $"DELETE FROM {table} WHERE {condition}";

        public ChangeRequestsVM(IMessageService messageService) : base(messageService) { }
    }
}