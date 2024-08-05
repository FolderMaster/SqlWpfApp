using System.Collections.Generic;
using System.Linq;

using ViewModel.Classes;
using ViewModel.Interfaces.Services.Files;

namespace View.Implementations.Dialogs
{
    public abstract class BaseFileDialogService : IGettingFileService
    {
        /// <inheritdoc/>
        public abstract string? GetFilePath
            (IEnumerable<FileFormat>? filter = null);

        protected string GetFilter(IEnumerable<FileFormat>? filter)
        {
            var result = "";
            if (filter != null)
            {
                var filterParts = new List<string>();
                foreach (var fileFormat in filter)
                {
                    filterParts.Add(fileFormat.Name + '|' +
                        (fileFormat.Extensions != null ? string.Join(';',
                        fileFormat.Extensions.Select((e) => "*." + e)) : ""));
                }
                result = string.Join('|', filterParts);
            }
            return result;
        }
    }
}
