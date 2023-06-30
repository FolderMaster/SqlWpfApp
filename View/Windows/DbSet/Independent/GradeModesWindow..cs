using System;
using System.Collections.Generic;

using Model.Independent;
using ViewModel.VMs.DbSet;
using View.Implementations.MessageBoxes;
using View.Services;
using ViewModel.Interfaces;

namespace View.Windows.DbSet.Independent
{
    public class GradeModesWindow : GridDbSetWindow
    {
        private static GradeModesWindow? _instance = null;

        private static Action _call = () =>
        {
            var instance = Instance;
            instance.Show();
        };

        public static GradeModesWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GradeModesWindow();
                }
                return _instance;
            }
        }

        public static Action Call => _call;

        public static IDataBaseContextCreator? DataBaseContextCreator { get; set; }

        private GradeModesWindow() : base()
        {
            Title = AppResourceService.GetHeader(nameof(GradeMode));
            Icon = AppResourceService.GetIcon(nameof(GradeMode));

            DataContext = new List<object>()
            {
                new DbSetVM<GradeMode>(DataBaseContextCreator, new ErrorMessageBoxService()),
                (string nameProperty) => nameProperty != nameof(GradeMode.Grades) &&
                nameProperty != nameof(GradeMode.StudyForms),
                (Action)(() => _instance = null)
            };
        }
    }
}