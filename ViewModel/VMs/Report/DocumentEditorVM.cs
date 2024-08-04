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

        private IDocument? _document;

        private ISelection? _selection;

        private object? _markerStyle;

        [ObservableProperty]
        private int columnsCount = 10;

        [ObservableProperty]
        private int rowsCount = 10;

        public IDocument? Document
        {
            get => _document;
            set => SetProperty(ref _document, value);
        }

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

        public object? MarkerStyle
        {
            get => _markerStyle;
            set
            {
                if (SetProperty(ref _markerStyle, value))
                {
                    CreateListCommand.NotifyCanExecuteChanged();
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
            CreateListCommand = new RelayCommand(() => Selection.InsertList(MarkerStyle),
                () => Selection != null && MarkerStyle != null);
            CreateTableCommand = new RelayCommand
                (() => Selection.InsertTable(rowsCount, columnsCount),
                () => Selection != null);
            CreateImageCommand = new RelayCommand(() =>
            {
                var filePath = GettingOpenFileService?.GetFilePath();
                if (filePath != null)
                {
                    var bitmap = new BitmapImage(new Uri(filePath));
                    Selection.InsertImage(bitmap);
                }
            }, () => Selection != null);
        }
    }
}
