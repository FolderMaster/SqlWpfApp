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
        private ICommand _saveCommand;

        private IMessageService _messageService;

        private DbContext _dbContext;

        private DbSet<T> _dbSet;

        public DbSet<T> DbSet
        {
            get => _dbSet;
            private set
            {
                _dbSet = value;
                try
                {
                    _dbSet.Load();
                }
                catch (Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            }
        }

        public ICommand SaveCommand => _saveCommand;

        public ObservableCollection<T> DataBaseLocal => DbSet.Local.ToObservableCollection();

        public DataBaseVM(IMessageService messageService, DbContext dbContext, DbSet<T> dbSet)
        {
            _messageService = messageService;
            _dbContext = dbContext;
            DbSet = dbSet;
            _saveCommand = new RelayCommand((parameter) =>
            {
                try
                {
                    _dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    _messageService.ShowMessage(ex.Message, "Error!");
                }
            });
        }
    }
}