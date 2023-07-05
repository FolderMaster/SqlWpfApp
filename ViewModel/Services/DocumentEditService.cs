using System.Data;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ViewModel.Services
{
    public class DocumentEditService
    {
        private static double _cellSpacing = 0;

        private static Brush _borderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        private static Thickness _borderThickness = new Thickness(1);

        private FlowDocument _document;

        public DocumentEditService(FlowDocument document) => _document = document;

        public void AddParagraph(string paragraph) =>
            _document.Blocks.Add(new Paragraph(new Run(paragraph)));

        public void AddTable(DataTable dataTable) => _document.Blocks.Add(CreateTable(dataTable));

        public void Clear() => _document.Blocks.Clear();

        private Table CreateTable(DataTable dataTable)
        {
            var table = new Table();
            table.CellSpacing = _cellSpacing;

            var rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            var header = new TableRow();
            rowGroup.Rows.Add(header);

            if(dataTable != null)
            {
                foreach (DataColumn column in dataTable.Columns)
                {
                    var tableColumn = new TableColumn();
                    table.Columns.Add(tableColumn);

                    var value = column.ColumnName;
                    header.Cells.Add(CreateTableCell(value));
                }
                foreach (DataRow row in dataTable.Rows)
                {
                    var tableRow = new TableRow();
                    rowGroup.Rows.Add(tableRow);

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        var value = row[column];
                        tableRow.Cells.Add(CreateTableCell(value));
                    }
                }
            }
            return table;
        }

        private TableCell CreateTableCell(object containedValue)
        {
            var cell = new TableCell(new Paragraph(new Run(containedValue.ToString())));
            cell.BorderBrush = _borderBrush;
            cell.BorderThickness = _borderThickness;
            return cell;
        }
    }
}