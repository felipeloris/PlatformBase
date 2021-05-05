using System.IO;
using System.IO.Compression;
using System.Text;

namespace Loris.Common.Tools
{
    public static class Compression
    {
        private static readonly Encoding DefaultEncoding = Encoding.GetEncoding(1252);

        public static byte[] GZip(string str)
        {
            byte[] output = DefaultEncoding.GetBytes(str);

            return GZip(output);
        }

        public static byte[] GZip(byte[] content)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(content, 0, content.Length);
                }
                return memory.ToArray();
            }
        }

        public static byte[] GZip(Stream content)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                content.CopyTo(memoryStream);
                return GZip(memoryStream.ToArray());
            }
        }

        public static string GUnZip(byte[] input, Encoding encoding)
        {
            if (encoding == null)
            {
                encoding = DefaultEncoding;
            }

            byte[] output = GUnZip(input);

            return encoding.GetString(output);
        }

        public static byte[] GUnZip(byte[] input)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(input), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        /// <summary>
        /// Checks the first two bytes in a GZIP file, which must be 31 and 139.
        /// </summary>
        public static bool IsGZipHeader(byte[] arr)
        {
            return arr.Length >= 2 &&
                arr[0] == 31 &&
                arr[1] == 139;
        }
    }
}
