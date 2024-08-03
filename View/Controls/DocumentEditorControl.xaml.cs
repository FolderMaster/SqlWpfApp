using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

using View.Implementations.Document;

using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Files;
using ViewModel.VMs.Report;

namespace View.Controls
{
    /// <summary>
    /// Interaction logic for DocumentEditorControl.xaml
    /// </summary>
    public partial class DocumentEditorControl : UserControl
    {
        public static DependencyProperty DocumentProperty =
            DependencyProperty.Register(nameof(Document), typeof(Document),
                typeof(DocumentEditorControl), new FrameworkPropertyMetadata(OnPropertyChanged));

        public static DependencyProperty DocumentServiceProperty =
            DependencyProperty.Register(nameof(DocumentService), typeof(IDocumentService),
                typeof(DocumentEditorControl), new FrameworkPropertyMetadata(OnPropertyChanged));

        public static DependencyProperty OpenGettingFileServiceProperty =
            DependencyProperty.Register(nameof(OpenGettingFileService), typeof(IGettingFileService),
                typeof(DocumentEditorControl), new FrameworkPropertyMetadata(OnPropertyChanged));

        public Document Document
        {
            get => (Document)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public IDocumentService? DocumentService
        {
            get => (IDocumentService)GetValue(DocumentServiceProperty);
            set => SetValue(DocumentServiceProperty, value);
        }

        public IGettingFileService? OpenGettingFileService
        {
            get => (IGettingFileService?)GetValue(OpenGettingFileServiceProperty);
            set => SetValue(OpenGettingFileServiceProperty, value);
        }

        public DocumentEditorControl()
        {
            InitializeComponent();

            var vm = new DocumentEditorVM();
            vm.PropertyChanged += Vm_PropertyChanged;
            DataContext = vm;
        }

        private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var vm = (DocumentEditorVM)sender;
            switch (e.PropertyName)
            {
                case nameof(DocumentEditorVM.DocumentService):
                    DocumentService = vm.DocumentService;
                    break;
                case nameof(DocumentEditorVM.GettingOpenFileService):
                    OpenGettingFileService = vm.GettingOpenFileService;
                    break;
            }
        }

        private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var control = (DocumentEditorControl)sender;
            var vm = (DocumentEditorVM)control.DataContext;
            if (e.Property == DocumentServiceProperty)
            {
                vm.DocumentService = (IDocumentService?)e.NewValue;
            }
            else if (e.Property == OpenGettingFileServiceProperty)
            {
                vm.GettingOpenFileService = (IGettingFileService?)e.NewValue;
            }
        }
    }
}
