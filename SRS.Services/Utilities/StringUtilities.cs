using System.Linq;

namespace SRS.Services.Utilities
{
    public static class StringUtilities
    {
        public static string JoinNotNullOrWhitespace(string separator, params string[] values)
        {
            return string.Join(separator, values.Where(x => !string.IsNullOrWhiteSpace(x)));
        }
    }
}