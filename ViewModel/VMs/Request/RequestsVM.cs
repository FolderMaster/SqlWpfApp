using CommunityToolkit.Mvvm.ComponentModel;
using System.Data.SQLite;
using System.Data;
using System;

using ViewModel.Interfaces;

namespace ViewModel.VMs.Request
{
    public partial class RequestsVM : ObservableObject
    {
        [ObservableProperty]
        private DataTable executingResult;

        public IDbContextCreator DataBaseContextCreator { get; private set; }

        public IMessageService MessageService { get; private set; }

        public RequestsVM(IDbContextCreator dataBaseContextCreator,
            IMessageService messageService)
        {
            MessageService = messageService;
            DataBaseContextCreator = dataBaseContextCreator;
        }

        protected void ExecuteSqlCommand(string sqlCommand)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection
                (DataBaseContextCreator.Result.ConnectionString))
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