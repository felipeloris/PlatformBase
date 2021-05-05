using Loris.Common.Cryptography;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Tools;
using System;

namespace Loris.Common.Helpers
{
    public static class LoginHelper
    {
        /// <summary>
        /// Tempo máximo de login com a mesma chave (em horas)
        /// </summary>
        private const int MaxLoginTime = 8;

        public static string EncriptLogin(ILogin login)
        {
            var jLogin = SerializeObject.ToJson<ILogin>(login);
            var encrypted = CipherUtility.AesEncrypt(jLogin);

            /*
            var encrypted = CipherUtility.AesEncrypt(sKeyValues);
            var bytes = Encoding.UTF8.GetBytes(encrypted);
            var zipped = SevenZipHelper.Compress(bytes);
            return Encoding.UTF8.GetString(zipped);
            */

            return encrypted;
        }

        public static ILogin DecriptLogin(string encryptedLogin)
        {
            /*
            var bytes = Encoding.UTF8.GetBytes(encryptedLogin);
            var unzipped = SevenZipHelper.Decompress(bytes);
            var encrypted = Encoding.UTF8.GetString(unzipped);
            var decrypted = CipherUtility.AesDecrypt(encrypted);
            */

            try
            {
                var decrypted = CipherUtility.AesDecrypt(encryptedLogin);
                var jLogin = SerializeObject.FromJson<BasicLogin>(decrypted);

                return jLogin;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Invalid Login!", ex);
            }
        }

        public static int GetHashCode(ILogin login)
        {
            unchecked
            {
                var result = (login.ExtenalId != null ? login.ExtenalId.GetHashCode() : 0);

                return result;
            }
        }

        public static string EncryptPassword(string password)
        {
            return Md5Cryptography.Encrypt(password, Constants.CryptographyKey, true);
            //return CipherUtility.AesEncrypt(password);
        }
    }
}
