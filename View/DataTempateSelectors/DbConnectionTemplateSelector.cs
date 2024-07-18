using System.Windows;
using System.Windows.Controls;

using ViewModel.Dependencies.DataBase.MsSqlServer;
using ViewModel.Dependencies.DataBase.Sqlite;

namespace View.DataTempateSelectors
{
    public class DbConnectionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MsSqlServerTemplate { get; set; }

        public DataTemplate SqliteTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch(item)
            {
                case MsSqlServerDbConnection:
                    return MsSqlServerTemplate;
                case SqliteDbConnection:
                    return SqliteTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
