using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ViewModel.Classes.Connections.MsSqlServer;
using ViewModel.Classes.Connections.Sqlite;
using ViewModel.VMs.Connections;

namespace View.Controls.Connections
{
    /// <summary>
    /// Interaction logic for DbConnectionControl.xaml
    /// </summary>
    public partial class SqliteDbConnectionControl : UserControl
    {
        public static DependencyProperty ConnectionsProperty =
            DependencyProperty.Register(nameof(Connections),
                typeof(IList<SqliteConnectionData>), typeof(SqliteDbConnectionControl),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        public static DependencyProperty SelectedConnectionProperty =
            DependencyProperty.Register(nameof(SelectedConnection), typeof(SqliteConnectionData),
                typeof(SqliteDbConnectionControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        public IList<SqliteConnectionData> Connections
        {
            get => (IList<SqliteConnectionData>)GetValue(ConnectionsProperty);
            set => SetValue(ConnectionsProperty, value);
        }

        public SqliteConnectionData SelectedConnection
        {
            get => (SqliteConnectionData)GetValue(SelectedConnectionProperty);
            set => SetValue(SelectedConnectionProperty, value);
        }

        public SqliteDbConnectionControl()
        {
            InitializeComponent();

            var vm = new SqliteConnectionVM();
            vm.PropertyChanged += Vm_PropertyChanged;
            DataContext = vm;

        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var vm = (SqliteConnectionVM)sender;
            switch (e.PropertyName)
            {
                case nameof(SqliteConnectionVM.Connections):
                    Connections = vm.Connections;
                    break;
                case nameof(SqliteConnectionVM.SelectedConnection):
                    SelectedConnection = vm.SelectedConnection;
                    break;
            }
        }

        private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (SqliteDbConnectionControl)sender;
            var vm = (SqliteConnectionVM)control.DataContext;
            if (e.Property == ConnectionsProperty)
            {
                vm.Connections = (IList<SqliteConnectionData>)e.NewValue;
            }
            else if (e.Property == SelectedConnectionProperty)
            {
                vm.SelectedConnection = (SqliteConnectionData)e.NewValue;
            }
        }
    }
}
