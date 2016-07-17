using System;
using System.Text;
using System.Security.Cryptography;

namespace MVCAuthorization.Utils
{
    public static class SHA1Encoder
    {
        public static string Encode(string stringToEncode)
        {
            SHA1 hash = SHA1.Create();
            var encoder = new ASCIIEncoding();
            byte[] array = encoder.GetBytes(stringToEncode ?? String.Empty);
            return BitConverter.ToString(hash.ComputeHash(array)).ToLower().Replace("-", "");
        }
    }
}