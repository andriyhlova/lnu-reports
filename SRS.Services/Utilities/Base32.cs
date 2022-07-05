using System;
using System.Linq;
using System.Text;

namespace SRS.Services.Utilities
{
    public static class Base32
    {
        private const string Base32AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public static string ToBase32String(this byte[] input, bool addPadding = true)
        {
            if (input == null || input.Length == 0)
            {
                return string.Empty;
            }

            var bits = input.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).Aggregate((a, b) => a + b).PadRight((int)(Math.Ceiling((input.Length * 8) / 5d) * 5), '0');
            var result = Enumerable.Range(0, bits.Length / 5).Select(i => Base32AllowedCharacters.Substring(Convert.ToInt32(bits.Substring(i * 5, 5), 2), 1)).Aggregate((a, b) => a + b);
            if (addPadding)
            {
                result = result.PadRight((int)(Math.Ceiling(result.Length / 8d) * 8), '=');
            }

            return result;
        }

        public static string EncodeAsBase32String(this string input, bool addPadding = true)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var bytes = Encoding.UTF8.GetBytes(input);
            return bytes.ToBase32String(addPadding);
        }

        public static string DecodeFromBase32String(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var bytes = input.ToByteArray();
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Converts a Base32 string into the corresponding byte array, using 5 bits per character.
        /// </summary>
        /// <param name="input">The Base32 String.</param>
        /// <returns>A byte array containing the properly encoded bytes.</returns>
        public static byte[] ToByteArray(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new byte[0];
            }

            var bits = input.TrimEnd('=').ToUpper().ToCharArray().Select(c => Convert.ToString(Base32AllowedCharacters.IndexOf(c), 2).PadLeft(5, '0')).Aggregate((a, b) => a + b);
            return Enumerable.Range(0, bits.Length / 8).Select(i => Convert.ToByte(bits.Substring(i * 8, 8), 2)).ToArray();
        }
    }
}