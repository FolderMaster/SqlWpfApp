using System.Collections.Generic;

using ViewModel.Interfaces.Services.Data;

namespace ViewModel.Interfaces.Services.Document
{
    public interface IDocument
    {
        public double PageWidth { get; set; }

        public double PageHeight { get; set; }

        public IRange ContentRange { get; }

        public IRange StartRange { get; }

        public IRange EndRange { get; }

        public object DocumentPaginator { get; }

        public void Replace(object? value, IRange range);

        public IEnumerable<IRange> Search(object pattern,
            IRange range, ISearchService searchService);
    }
}
