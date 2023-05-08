using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace SQLiteWpfApp.ViewModels.VMs
{
    public class DataBaseVM<T> where T : class
    {
        protected IMessageService _messageService;

        protected DataBaseContext _dbContext;

        protected DbSet<T> _dbSet;

        protected DbSet<T> DbSet
        {
            get => _dbSet;
            set
            {
                _dbSet = value;
                try
                {
                    _dbSet.Load();
                    DataBaseLocal = DbSet.Local.ToObservableCollection();
                }
                catch (Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            }
        }

        public ICommand SaveCommand { get; private set; }

        public ObservableCollection<T> DataBaseLocal { get; private set; }

        public DataBaseVM(IMessageService messageService, DataBaseContext dbContext,
            DbSet<T> dbSet)
        {
            _messageService = messageService;
            _dbContext = dbContext;
            DbSet = dbSet;
            SaveCommand = new RelayCommand((parameter) =>
            {
                try
                {
                    _dbContext.SaveChanges<T>();
                }
                catch(Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            });
        }
    }
}