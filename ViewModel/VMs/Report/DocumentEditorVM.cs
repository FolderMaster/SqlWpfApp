using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Windows.Media.Imaging;

using ViewModel.Interfaces.Services.Document;
using ViewModel.Interfaces.Services.Files;

namespace ViewModel.VMs.Report
{
    public partial class DocumentEditorVM : ObservableObject
    {
        private const double _delta = 2;

        private ISelection? _selection;

        [ObservableProperty]
        private object? markerStyle;

        [ObservableProperty]
        private int columnsCount = 10;

        [ObservableProperty]
        private int rowsCount = 10;

        public ISelection? Selection
        {
            get => _selection;
            set
            {
                if (SetProperty(ref _selection, value))
                {
                    CreateListCommand.NotifyCanExecuteChanged();
                    CreateTableCommand.NotifyCanExecuteChanged();
                    CreateImageCommand.NotifyCanExecuteChanged();
                    IncreaseSizeCommand.NotifyCanExecuteChanged();
                    DecreaseSizeCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IGettingFileService? GettingOpenFileService { get; set; }

        public IDocumentService? DocumentService { get; set; }

        public RelayCommand IncreaseSizeCommand { get; private set; }

        public RelayCommand DecreaseSizeCommand { get; private set; }

        public RelayCommand CreateListCommand { get; private set; }

        public RelayCommand CreateTableCommand { get; private set; }

        public RelayCommand CreateImageCommand { get; private set; }

        public DocumentEditorVM()
        {
            IncreaseSizeCommand = new RelayCommand(() => Selection.FontSize += _delta,
                () => Selection != null);
            DecreaseSizeCommand = new RelayCommand(() => Selection.FontSize -= _delta,
                () => Selection != null);
            CreateListCommand = new RelayCommand(() => Selection.CreateList(MarkerStyle),
                () => Selection != null);
            CreateTableCommand = new RelayCommand
                (() => Selection.CreateTable(rowsCount, columnsCount),
                () => Selection != null);
            CreateImageCommand = new RelayCommand(() =>
            {
                var filePath = GettingOpenFileService?.GetFilePath();
                if (filePath != null)
                {
                    var bitmap = new BitmapImage(new Uri(filePath));
                    Selection.CreateImage(bitmap);
                }
            }, () => Selection != null);
        }
    }
}
