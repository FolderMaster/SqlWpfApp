using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace View.Services
{
    public static class DocumentEditorService
    {
        private const double _cellSpacing = 0;

        private static readonly Brush _borderBrush =
            new SolidColorBrush(Color.FromRgb(0, 0, 0));

        private static readonly Thickness _borderThickness = new Thickness(1);

        public static List CreateList(int itemsCount)
        {
            var list = new List();
            for (int i = 0; i < itemsCount; ++i)
            {
                list.ListItems.Add(new ListItem(new Paragraph()));
            }
            return list;
        }

        public static Table CreateTable(int columnsCount, int rowsCount)
        {
            var table = new Table();
            table.CellSpacing = _cellSpacing;
            for (int i = 0; i < columnsCount; ++i)
            {
                var column = new TableColumn();
                table.Columns.Add(new TableColumn());
                
            }
            table.RowGroups.Add(new TableRowGroup());
            for (int i = 0; i < rowsCount; ++i)
            {
                var row = new TableRow();
                for (int j = 0; j < columnsCount; ++j)
                {
                    row.Cells.Add(CreateTableCell(new Paragraph()));
                }
                table.RowGroups[0].Rows.Add(row);
            }
            return table;
        }

        public static Table CreateTable(DataTable dataTable)
        {
            var table = new Table();
            table.CellSpacing = _cellSpacing;

            var rowGroup = new TableRowGroup();
            table.RowGroups.Add(rowGroup);

            var header = new TableRow();
            rowGroup.Rows.Add(header);
            foreach (DataColumn column in dataTable.Columns)
            {
                var tableColumn = new TableColumn();
                table.Columns.Add(tableColumn);

                header.Cells.Add(CreateTableCell
                    (new Paragraph(new Run(column.ColumnName))));
            }
            foreach (DataRow row in dataTable.Rows)
            {
                var tableRow = new TableRow();
                rowGroup.Rows.Add(tableRow);
                foreach (DataColumn column in dataTable.Columns)
                {
                    var value = row[column];
                    tableRow.Cells.Add(CreateTableCell
                        (new Paragraph(new Run(value.ToString()))));
                }
            }


            return table;
        }

        public static BlockUIContainer CreateImage(ImageSource imageSource)
        {
            var image = new Image()
            {
                Source = imageSource,
                Width = imageSource.Width,
                Height = imageSource.Height
            };
            var container = new BlockUIContainer(image);
            container.MouseDown += BlockUIContainer_MouseDown;
            return container;
        }

        public static void FillListBlock(List list, IEnumerable<object> items)
        {
            for (int i = 0; i < list.ListItems.Count; ++i)
            {
                var paragraph = (Paragraph)list.ListItems.ElementAt(i).Blocks.FirstBlock;
                paragraph.Inlines.Clear();
                paragraph.Inlines.Add(new Run(items.ElementAt(i).ToString()));
            }
        }

        private static TableCell CreateTableCell(Block block) =>
            new TableCell(block)
            {
                BorderBrush = _borderBrush,
                BorderThickness = _borderThickness
            };

        public static void SetBlock(Block textElement, TextPointer start, TextPointer end)
        {
            end.Paragraph.SiblingBlocks.
                InsertAfter(end.Paragraph, textElement);
            var textRange = new TextRange(start, end);
            textRange.Text = "";
        }

        private static AdornerLayer? GetAdornerLayer(FrameworkContentElement content)
        {
            var current = content.Parent;
            while (current != null)
            {
                if (current is RichTextBox richTextBox)
                {
                    return AdornerLayer.GetAdornerLayer(richTextBox);
                }
                else if (current is FrameworkContentElement element)
                {
                    current = element.Parent;
                }
            }
            return null;
        }

        private static void BlockUIContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var container = (BlockUIContainer)sender;
            var image = (Image)container.Child;
            var adornerLayer = GetAdornerLayer(container);
            var adorner = (Adorner)image.Tag;
            if (adorner == null)
            {
                adorner = new ResizeAdorner(image);
                image.Tag = adorner;
                adornerLayer.Add(adorner);
            }
            else
            {
                image.Tag = null;
                adornerLayer.Remove(adorner);
            }
        }
    }
}
