﻿using System.Windows.Controls;
using System.Windows.Documents;
using ViewModel.Interfaces.Services.Document;

namespace View.Implementations.Dialogs
{
    /// <summary>
    /// Класс сервиса диалога печати с методом печати документа. Реализует
    /// <see cref="IPrintService"/>.
    /// </summary>
    public class PrintDialogService : IPrintService
    {
        /// <summary>
        /// Печатает документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="description">Описание.</param>
        public void Print(object document, string description)
        {
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() == true)
            {
                dialog.PrintDocument(document as DocumentPaginator, description);
            }
        }
    }
}