using System;
using System.Collections.Generic;

using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public class ScholarshipsWindow : GridDbSetWindow
    {
        private static ScholarshipsWindow? _instance = null;

        private static Action _call = () =>
            {
                var instance = Instance;
                instance.Show();
            };

        public static ScholarshipsWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScholarshipsWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private ScholarshipsWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(Scholarship));
            Icon = AppResourceService.GetIcon(nameof(Scholarship));

            DataContext = new List<object>()
            {
                new DbSetVM<Scholarship>(DataBaseContextCreator, new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(Scholarship.Students),
                (Action)(() => _instance = null)
            };
        }
    }
}