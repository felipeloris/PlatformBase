using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Loris.Common.Cryptography
{
    public static class Hasher
    {
        public static string Sha1(string text)
        {
            var result = default(string);
            using (var algo = new SHA1Managed())
            {
                result = GenerateHashString(algo, text);
            }
            return result;
        }

        public static string Sha256(string text)
        {
            var result = default(string);
            using (var algo = new SHA256Managed())
            {
                result = GenerateHashString(algo, text);
            }
            return result;
        }

        public static string Sha384(string text)
        {
            var result = default(string);
            using (var algo = new SHA384Managed())
            {
                result = GenerateHashString(algo, text);
            }
            return result;
        }

        public static string Sha512(string text)
        {
            var result = default(string);
            using (var algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, text);
            }
            return result;
        }

        public static string MD5(string text)
        {
            var result = default(string);
            using (var algo = new MD5CryptoServiceProvider())
            {
                result = GenerateHashString(algo, text);
            }
            return result;
        }

        public static Dictionary<string, Func<string, string>> Hashers()
        {
            return new Dictionary<string, Func<string, string>>
                {
                    { nameof(MD5).ToLower(), MD5},
                    { nameof(Sha1).ToLower(), Sha1},
                    { nameof(Sha256).ToLower(), Sha256},
                    { nameof(Sha384).ToLower(), Sha384},
                    { nameof(Sha512).ToLower(), Sha512},
                };
        }

        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            // Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get has value in array of bytes
            var result = algo.Hash;

            // Return as hexadecimal string
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }

        public static string GetChecksum(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var sha = new SHA256Managed();
                var checksum = sha.ComputeHash(stream);
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
        }

        public static string GetChecksumBuffered(Stream stream)
        {
            using (var bufferedStream = new BufferedStream(stream, 1024 * 32))
            {
                var sha = new SHA256Managed();
                var checksum = sha.ComputeHash(bufferedStream);
                return BitConverter.ToString(checksum).Replace("-", string.Empty);
            }
        }
    }
}
