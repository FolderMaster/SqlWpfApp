using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Data.SQLite;
using System.Data;

using SQLiteWpfApp.ViewModels.Services;


namespace SQLiteWpfApp.ViewModels.VMs
{
    public partial class RequestsVM : ObservableObject
    {
        [ObservableProperty]
        private DataTable executingResult;

        public IMessageService MessageService { get; private set; }

        public RequestsVM(IMessageService messageService) => MessageService = messageService;

        protected void ExecuteSqlCommand(string sqlCommand)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection
                (DataBaseContext.DataBaseConnection))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(sqlCommand, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            ExecutingResult = dataTable;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageService?.ShowMessage(ex.Message, "Error!");
            }
        }
    }
}