using System;
using System.Security.Cryptography;
using System.Text;

namespace Loris.Common.Cryptography
{
    public sealed class Md5Cryptography
    {
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            var tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string cipherString, string key, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            var toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider
                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            var tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }

        public static string GetHash(string input)
        {
            var sBuilder = new StringBuilder();

            using (var md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (var i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(i.ToString("x2"));
                }
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
