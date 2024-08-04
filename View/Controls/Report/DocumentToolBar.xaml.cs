using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using View.Implementations.Document;

namespace View.Controls.Report
{
    public partial class DocumentToolBar : UserControl
    {
        public static DependencyProperty SelectionProperty = DependencyProperty.Register
            (nameof(Selection), typeof(Selection), typeof(DocumentToolBar));

        public static DependencyProperty DocumentProperty = DependencyProperty.Register
            (nameof(Document), typeof(Document), typeof(DocumentToolBar));

        public static DependencyProperty FamiliesProperty = DependencyProperty.Register
            (nameof(Families), typeof(IEnumerable), typeof(DocumentToolBar));

        public static DependencyProperty IncreaseSizeCommandProperty = DependencyProperty.Register
            (nameof(IncreaseSizeCommand), typeof(ICommand), typeof(DocumentToolBar));

        public static DependencyProperty DecreaseSizeCommandProperty = DependencyProperty.Register
            (nameof(DecreaseSizeCommand), typeof(ICommand), typeof(DocumentToolBar));

        public static DependencyProperty AlignmentsProperty = DependencyProperty.Register
            (nameof(Alignments), typeof(IEnumerable), typeof(DocumentToolBar));

        public static DependencyProperty CreateImageCommandProperty = DependencyProperty.Register
            (nameof(CreateImageCommand), typeof(ICommand), typeof(DocumentToolBar));

        public static DependencyProperty CreateListCommandProperty = DependencyProperty.Register
            (nameof(CreateListCommand), typeof(ICommand), typeof(DocumentToolBar));

        public static DependencyProperty MarkerStylesProperty = DependencyProperty.Register
            (nameof(MarkerStyles), typeof(IEnumerable), typeof(DocumentToolBar));

        public static DependencyProperty SelectedMarkerStyleProperty = DependencyProperty.Register
            (nameof(SelectedMarkerStyle), typeof(object), typeof(DocumentToolBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static DependencyProperty CreateTableCommandProperty = DependencyProperty.Register
            (nameof(CreateTableCommand), typeof(ICommand), typeof(DocumentToolBar));

        public static DependencyProperty ColumnsCountProperty = DependencyProperty.Register
            (nameof(ColumnsCount), typeof(int), typeof(DocumentToolBar),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static DependencyProperty RowsCountProperty = DependencyProperty.Register
            (nameof(RowsCount), typeof(int), typeof(DocumentToolBar),
            new FrameworkPropertyMetadata(10, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Selection Selection
        {
            get => (Selection)GetValue(SelectionProperty);
            set => SetValue(SelectionProperty, value);
        }

        public Document Document
        {
            get => (Document)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public IEnumerable Families
        {
            get => (IEnumerable)GetValue(FamiliesProperty);
            set => SetValue(FamiliesProperty, value);
        }

        public ICommand IncreaseSizeCommand
        {
            get => (ICommand)GetValue(IncreaseSizeCommandProperty);
            set => SetValue(IncreaseSizeCommandProperty, value);
        }

        public ICommand DecreaseSizeCommand
        {
            get => (ICommand)GetValue(DecreaseSizeCommandProperty);
            set => SetValue(DecreaseSizeCommandProperty, value);
        }

        public IEnumerable Alignments
        {
            get => (IEnumerable)GetValue(AlignmentsProperty);
            set => SetValue(AlignmentsProperty, value);
        }

        public ICommand CreateImageCommand
        {
            get => (ICommand)GetValue(CreateImageCommandProperty);
            set => SetValue(CreateImageCommandProperty, value);
        }

        public ICommand CreateListCommand
        {
            get => (ICommand)GetValue(CreateListCommandProperty);
            set => SetValue(CreateListCommandProperty, value);
        }

        public IEnumerable MarkerStyles
        {
            get => (IEnumerable)GetValue(MarkerStylesProperty);
            set => SetValue(MarkerStylesProperty, value);
        }

        public object SelectedMarkerStyle
        {
            get => GetValue(SelectedMarkerStyleProperty);
            set => SetValue(SelectedMarkerStyleProperty, value);
        }

        public ICommand CreateTableCommand
        {
            get => (ICommand)GetValue(CreateTableCommandProperty);
            set => SetValue(CreateTableCommandProperty, value);
        }

        public int ColumnsCount
        {
            get => (int)GetValue(ColumnsCountProperty);
            set => SetValue(ColumnsCountProperty, value);
        }

        public int RowsCount
        {
            get => (int)GetValue(RowsCountProperty);
            set => SetValue(RowsCountProperty, value);
        }

        public DocumentToolBar()
        {
            InitializeComponent();
        }
    }
}
