using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Loris.Common.Cryptography
{
    public static class CipherUtility
    {
        internal static string Encrypt<T>(string value, string password, string salt)
             where T : SymmetricAlgorithm, new()
        {
            var rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            var algorithm = new T();

            var rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            var rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            var transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream())
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (var writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        internal static string Decrypt<T>(string text, string password, string salt)
           where T : SymmetricAlgorithm, new()
        {
            var rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            var algorithm = new T();

            var rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            var rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            var transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            using (var buffer = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (var stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                {
                    using (var reader = new StreamReader(stream, Encoding.Unicode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public static string AesEncrypt(string text, string cryptographyKey, string authorizationKey)
        {
            var valueOut = Encrypt<AesManaged>(text, cryptographyKey, authorizationKey);
            return valueOut;
        }

        public static string AesEncrypt(string text)
        {
            return AesEncrypt(text, Constants.CryptographyKey, Constants.AuthorizationKey);
        }

        public static string AesDecrypt(string text, string cryptographyKey, string authorizationKey)
        {
            var valueOut = Decrypt<AesManaged>(text, cryptographyKey, authorizationKey);
            return valueOut;
        }

        public static string AesDecrypt(string text)
        {
            return AesDecrypt(text, Constants.CryptographyKey, Constants.AuthorizationKey);
        }
    }
}
