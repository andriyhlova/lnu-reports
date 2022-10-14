using System;

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
    }
}