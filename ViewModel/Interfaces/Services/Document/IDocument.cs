using System.Collections.Generic;

namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocument
    {
        public double PageWidth { get; set; }

        public double PageHeight { get; set; }

        public object DocumentPaginator { get; }

        public object Range { get; }

        public object EndRange { get; }

        public void Clear();

        public void Replace(object? value, object range);

        public IEnumerable<object> Search(object value);
    }
}
