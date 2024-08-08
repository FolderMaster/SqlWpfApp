using System.Collections.Generic;

using ViewModel.Classes;

namespace ViewModel.Interfaces.Services.Data
{
    public interface ISearchService
    {
        public bool IsMatch(object value, object pattern);

        public IEnumerable<PatternMatch> GetMatches(object value, object pattern);

        public IEnumerable<IEnumerable<object>> GetGroups(object value, object pattern);
    }
}
