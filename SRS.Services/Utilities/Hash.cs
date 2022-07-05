using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SRS.Services.Utilities
{
    public static class Hash
    {
        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Vulnerability", "S5547:Cipher algorithms should be robust", Justification = "Old algorithm")]
        public static string Crypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Base32.ToBase32String(outputBuffer);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Vulnerability", "S5547:Cipher algorithms should be robust", Justification = "Old algorithm")]
        public static string Decrypt(this string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Base32.ToByteArray(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }
}