using System.Collections;

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
            document.Clear();
            foreach (var value in template)
            {
                document.InsertValue(value);
            }
        }
    }
}