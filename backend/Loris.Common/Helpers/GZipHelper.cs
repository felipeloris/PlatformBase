using System;
using System.IO;
using System.IO.Compression;

namespace Loris.Common.Helpers
{
    public static class GZipHelper
    {
        private static readonly int BUFFER_SIZE = 64 * 1024; //64kB

        public static byte[] Compress(byte[] inputData)
        {
            if (inputData == null)
            {
                throw new ArgumentNullException("InputData must be non-null!");
            }

            using (var compressIntoMs = new MemoryStream())
            {
                using (var gzs = new BufferedStream(new GZipStream(
                    compressIntoMs, CompressionMode.Compress), BUFFER_SIZE))
                {
                    gzs.Write(inputData, 0, inputData.Length);
                }
                return compressIntoMs.ToArray();
            }
        }

        public static byte[] Decompress(byte[] inputData)
        {
            if (inputData == null)
            {
                throw new ArgumentNullException("InputData must be non-null!");
            }

            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (var gzs = new BufferedStream(new GZipStream(
                        compressedMs, CompressionMode.Decompress), BUFFER_SIZE))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }
    }
}
