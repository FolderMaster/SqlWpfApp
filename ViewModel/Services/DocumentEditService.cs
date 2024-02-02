using System.Data;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ViewModel.Services
{
    /// <summary>
    /// Класс сервиса изменения документа с методами.
    /// </summary>
    public class DocumentEditService
    {
        /// <summary>
        /// Интервал между ячейками.
        /// </summary>
        private static double _cellSpacing = 0;

        /// <summary>
        /// Кисть границ ячеек.
        /// </summary>
        private static Brush _cellBorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        /// <summary>
        /// Толщина границ ячеек.
        /// </summary>
        private static Thickness _cellBorderThickness = new Thickness(1);

        /// <summary>
        /// Документ.
        /// </summary>
        private FlowDocument _document;

        /// <summary>
        /// Создаёт экземпляр класса <see cref="DocumentEditService"/>,
        /// </summary>
        /// <param name="document">Документ.</param>
        public DocumentEditService(FlowDocument document) => _document = document;

        /// <summary>
        /// Добавляет параграф документу.
        /// </summary>
        /// <param name="containedValue">Значение для размещения.</param>
        public void AddParagraph(object containedValue) =>
            _document.Blocks.Add(new Paragraph(new Run(containedValue.ToString())));

        /// <summary>
        /// Добавляет таблицу документу.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        public void AddTable(DataTable dataTable) => _document.Blocks.Add(CreateTable(dataTable));

        /// <summary>
        /// Очищает документ.
        /// </summary>
        public void Clear() => _document.Blocks.Clear();

        /// <summary>
        /// Создаёт таблицу.
        /// </summary>
        /// <param name="dataTable">Таблица данных.</param>
        /// <returns>Таблица на основе таблицы данных.</returns>
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

        /// <summary>
        /// Создаёт ячейку таблицы.
        /// </summary>
        /// <param name="containedValue">Значение для размещения.</param>
        /// <returns>Ячейка таблицы.</returns>
        private TableCell CreateTableCell(object containedValue)
        {
            var paragraph = new Paragraph(new Run(containedValue.ToString())
                { BaselineAlignment = BaselineAlignment.TextBottom })
            {
                TextAlignment = TextAlignment.Center
            };
            return new TableCell(paragraph)
            {
                BorderBrush = _cellBorderBrush,
                BorderThickness = _cellBorderThickness
            };
        }
    }
}