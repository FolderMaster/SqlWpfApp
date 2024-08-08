using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

using ViewModel.Interfaces.Services.Data;
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

        private IEnumerable<IRange>? _findedRanges;

        private string _pattern;

        [ObservableProperty]
        private string replacement;

        [ObservableProperty]
        private int columnsCount = 10;

        [ObservableProperty]
        private int rowsCount = 10;

        public IDocument? Document
        {
            get => _document;
            set
            {
                if (SetProperty(ref _document, value))
                {
                    SearchCommand.NotifyCanExecuteChanged();
                    ReplaceCommand.NotifyCanExecuteChanged();
                    CancelHighlightingCommand.NotifyCanExecuteChanged();
                }
            }
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

        public string Pattern
        {
            get => _pattern;
            set
            {
                if (SetProperty(ref _pattern, value))
                {
                    SearchCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public IEnumerable<IRange>? FindedRanges
        {
            get => _findedRanges;
            private set
            {
                var oldValue = _findedRanges;
                if (SetProperty(ref _findedRanges, value))
                {
                    if (oldValue != null)
                    {
                        foreach (var range in oldValue)
                        {
                            range.IsHighlighted = false;
                        }
                    }
                    if (FindedRanges != null)
                    {
                        foreach (var range in FindedRanges)
                        {
                            range.IsHighlighted = true;
                        }
                    }
                    ReplaceCommand.NotifyCanExecuteChanged();
                    CancelHighlightingCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ISearchService SearchService { get; set; }

        public IGettingFileService GettingOpenFileService { get; set; }

        public IDocumentService DocumentService { get; set; }

        public RelayCommand IncreaseSizeCommand { get; private set; }

        public RelayCommand DecreaseSizeCommand { get; private set; }

        public RelayCommand CreateListCommand { get; private set; }

        public RelayCommand CreateTableCommand { get; private set; }

        public RelayCommand CreateImageCommand { get; private set; }

        public RelayCommand SearchCommand { get; private set; }

        public RelayCommand ReplaceCommand { get; private set; }

        public RelayCommand CancelHighlightingCommand { get; private set; }

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
            SearchCommand = new RelayCommand(() =>
            {
                FindedRanges = null;
                FindedRanges = Document.Search(Pattern, Document.ContentRange, SearchService);
            }, () => Document != null && !string.IsNullOrEmpty(Pattern));
            ReplaceCommand = new RelayCommand(() =>
            {
                foreach (var range in FindedRanges)
                {
                    Document.Replace(Replacement, range);
                }
            }, () => Document != null && FindedRanges != null && FindedRanges.Any());
            CancelHighlightingCommand = new RelayCommand(() => FindedRanges = null,
                () => Document != null && FindedRanges != null && FindedRanges.Any());
        }
    }
}
