﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Loris.Common.Cryptography
{
    /// <summary>
    /// Hash CRC-32
    /// Use:
    /// var crc32 = new Crc32();
    /// var hash = crc32.Get(Encoding.UTF8.GetBytes(text)).ToString("X");
    /// </summary>
    public class Crc32
    {
        #region Constants
        /// <summary>
        /// Generator polynomial (modulo 2) for the reversed CRC32 algorithm. 
        /// </summary>
        private const uint s_generator = 0xEDB88320;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of the Crc32 class.
        /// </summary>
        public Crc32()
        {
            // Constructs the checksum lookup table. Used to optimize the checksum.
            _checksumTable = Enumerable.Range(0, 256).Select(i =>
            {
                var tableEntry = (uint)i;
                for (var j = 0; j < 8; ++j)
                {
                    tableEntry = ((tableEntry & 1) != 0)
                        ? (s_generator ^ (tableEntry >> 1))
                        : (tableEntry >> 1);
                }
                return tableEntry;
            }).ToArray();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Calculates the checksum of the byte stream.
        /// </summary>
        /// <param name="byteStream">The byte stream to calculate the checksum for.</param>
        /// <returns>A 32-bit reversed checksum.</returns>
        public uint Get<T>(IEnumerable<T> byteStream)
        {
            try
            {
                // Initialize checksumRegister to 0xFFFFFFFF and calculate the checksum.
                return ~byteStream.Aggregate(0xFFFFFFFF, (checksumRegister, currentByte) =>
                          (_checksumTable[(checksumRegister & 0xFF) ^ Convert.ToByte(currentByte)] ^ (checksumRegister >> 8)));
            }
            catch (FormatException e)
            {
                throw new CrcException("Could not read the stream out as bytes.", e);
            }
            catch (InvalidCastException e)
            {
                throw new CrcException("Could not read the stream out as bytes.", e);
            }
            catch (OverflowException e)
            {
                throw new CrcException("Could not read the stream out as bytes.", e);
            }
        }
        #endregion

        #region Fields
        /// <summary>
        /// Contains a cache of calculated checksum chunks.
        /// </summary>
        private readonly uint[] _checksumTable;

        #endregion
    }

    [Serializable]
    internal class CrcException : Exception
    {
        public CrcException()
        {
        }

        public CrcException(string message) : base(message)
        {
        }

        public CrcException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CrcException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
