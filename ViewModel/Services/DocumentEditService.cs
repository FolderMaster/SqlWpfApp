using System.Collections;
using System.Data;

using ViewModel.Enums;
using ViewModel.Interfaces.Services.Document;

namespace ViewModel.Services
{
    /// <summary>
    /// Класс сервиса изменения документа с методами.
    /// </summary>
    public static class DocumentEditService
    {
        public static void ApplyTemplate(IDocument document, IEnumerable template)
        {
            document.Replace(null, document.ContentRange);
            foreach (var value in template)
            {
                document.Replace(value, document.EndRange);
            }
        }

        public static void InsertDataTable(IDocument document,
            IRange range, DataTable dataTable, DataFormat format)
        {
            document.Replace(dataTable, range);
        }
    }
}