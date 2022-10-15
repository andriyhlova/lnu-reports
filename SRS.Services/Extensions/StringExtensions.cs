using System;
using System.Text.RegularExpressions;

namespace SRS.Services.Extensions
{
    public static class StringExtensions
    {
        public static string TransformFirstLetter(this string text, Func<char, char> func)
        {
            if (text?.Length > 0)
            {
                return func(text[0]) + (text.Length > 1 ? text.Substring(1) : string.Empty);
            }

            return text;
        }

        public static string NormalizeText(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var result = text.Trim().ToUpper();
            var spaceRegex = new Regex(@"\s{2,}");
            return spaceRegex.Replace(result, " ");
        }
    }
}