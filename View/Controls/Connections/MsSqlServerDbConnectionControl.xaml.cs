using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using ViewModel.Classes.Connections.MsSqlServer;
using ViewModel.VMs.Connections;

namespace View.Controls.Connections
{
    /// <summary>
    /// Interaction logic for MsSqlServerDbConnectionControl.xaml
    /// </summary>
    public partial class MsSqlServerDbConnectionControl : UserControl
    {
        public static DependencyProperty ConnectionsDataProperty =
            DependencyProperty.Register(nameof(ConnectionsData),
                typeof(IList<MsSqlServerConnectionDataSet>), typeof(MsSqlServerDbConnectionControl),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        public static DependencyProperty SelectedConnectionProperty =
            DependencyProperty.Register(nameof(SelectedConnection), typeof(MsSqlServerConnectionData),
                typeof(MsSqlServerDbConnectionControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        public static DependencyProperty SelectedCredentialProperty =
            DependencyProperty.Register(nameof(SelectedCredential), typeof(MsSqlServerCredentialData),
                typeof(MsSqlServerDbConnectionControl), new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        public IList<MsSqlServerConnectionDataSet> ConnectionsData
        {
            get => (IList<MsSqlServerConnectionDataSet>)GetValue(ConnectionsDataProperty);
            set => SetValue(ConnectionsDataProperty, value);
        }

        public MsSqlServerConnectionData SelectedConnection
        {
            get => (MsSqlServerConnectionData)GetValue(SelectedConnectionProperty);
            set => SetValue(SelectedConnectionProperty, value);
        }

        public MsSqlServerCredentialData SelectedCredential
        {
            get => (MsSqlServerCredentialData)GetValue(SelectedCredentialProperty);
            set => SetValue(SelectedCredentialProperty, value);
        }

        public MsSqlServerDbConnectionControl()
        {
            InitializeComponent();

            var vm = new MsSqlServerConnectionVM();
            vm.PropertyChanged += Vm_PropertyChanged;
            DataContext = vm;
        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var vm = (MsSqlServerConnectionVM)sender;
            switch (e.PropertyName)
            {
                case nameof(MsSqlServerConnectionVM.ConnectionsData):
                    ConnectionsData = vm.ConnectionsData;
                    break;
                case nameof(MsSqlServerConnectionVM.SelectedConnection):
                    SelectedConnection = vm.SelectedConnection;
                    break;
                case nameof(MsSqlServerConnectionVM.SelectedCredential):
                    SelectedCredential = vm.SelectedCredential;
                    break;
            }
        }

        private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (MsSqlServerDbConnectionControl)sender;
            var vm = (MsSqlServerConnectionVM)control.DataContext;
            if (e.Property == ConnectionsDataProperty)
            {
                vm.ConnectionsData = (IList<MsSqlServerConnectionDataSet>)e.NewValue;
            }
            else if (e.Property == SelectedConnectionProperty)
            {
                vm.SelectedConnection = (MsSqlServerConnectionData)e.NewValue;
            }
            else if (e.Property == SelectedCredentialProperty)
            {
                vm.SelectedCredential = (MsSqlServerCredentialData)e.NewValue;
            }
        }
    }
}
