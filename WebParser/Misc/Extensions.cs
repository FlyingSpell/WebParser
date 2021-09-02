using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace WebParser
{
    public static class StringExtensions
    {
        public static String ParseBetween(this String source, String start, String end, bool greedy = true)
        {
            String grd = greedy ? String.Empty : "?";

            String pattern = $"(?<={start})" +
                             $"(.*{grd})" +
                             $"(?={end})";

            Match match = Regex.Match(source, pattern, RegexOptions.Singleline);

            return match.Value;
        }

        public static String[] ParseArray(this String source, String itemStart)
        {
            String pattern = $"(?<={itemStart})" +
                             $"(.*?)" +
                             $"(?={itemStart}|$)";

            MatchCollection matchCollection = Regex.Matches(source, pattern, RegexOptions.Singleline);

            return matchCollection.Select(match => match.Value).ToArray();
        }
    }
}
