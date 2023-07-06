using ViewModel.Interfaces;

namespace ViewModel.VMs.Request
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

        public ChangeRequestsVM(IDbContextBuilder dataBaseContextBuilder,
            IResourceService resourceService, IMessageService messageService) :
            base(dataBaseContextBuilder, resourceService, messageService) { }
    }
}