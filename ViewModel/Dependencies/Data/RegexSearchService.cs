using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using ViewModel.Classes;
using ViewModel.Interfaces.Services.Data;

namespace ViewModel.Dependencies.Data
{
    public class RegexSearchService : ISearchService
    {
        public bool IsMatch(object value, object pattern)
        {
            var regex = CreateRegex(pattern);
            var valueText = GetValueText(value);
            return regex.IsMatch(valueText);
        }

        public IEnumerable<PatternMatch> GetMatches(object value, object pattern)
        {
            var regex = CreateRegex(pattern);
            var valueText = GetValueText(value);
            return regex.Matches(valueText).Select((m) => new PatternMatch
                (m.Value, m.Index, m.Length));
        }

        public IEnumerable<IEnumerable<object>> GetGroups(object value, object pattern)
        {
            var regex = CreateRegex(pattern);
            var valueText = GetValueText(value);
            return regex.Matches(valueText).Select((m) => m.Groups.Values.Select((g) => g.Value));
        }

        private string GetValueText(object value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            var valueText = value.ToString();
            ArgumentNullException.ThrowIfNull(valueText, nameof(valueText));
            return valueText;
        }

        private Regex CreateRegex(object pattern)
        {
            ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));
            var patternText = pattern.ToString();
            ArgumentNullException.ThrowIfNull(patternText, nameof(patternText));
            return new Regex(patternText, RegexOptions.IgnoreCase);
        }
    }
}
