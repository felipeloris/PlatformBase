using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loris.Common.Helpers;
using System.Text;
using SevenZip;

namespace Loris.Common.Test.Tools
{
    [TestClass]
    public class CompressionTest
    {
        private string OriginalText = @"
            From A V8 engine is a V engine with eight cylinders mounted on the crankcase
            in two banks of four cylinders, in most cases set at a right angle to each other
            but sometimes at a narrower angle, with all eight pistons driving a common crankshaft.
            In its simplest form, it is basically two straight-4 engines sharing a common
            crankshaft. However, this simple configuration, with a single-plane crankshaft,
            has the same secondary dynamic imbalance problems as two straight-4s, resulting
            in annoying vibrations in large engine displacements. As a result, since the 1920s
            most V8s have used the somewhat more complex crossplane crankshaft with heavy
            counterweights to eliminate the vibrations. This results in an engine which is
            smoother than a V6, while being considerably less expensive than a V12 engine.
            Racing V8s continue to use the single plane crankshaft because it allows faster
            acceleration and more efficient exhaust system designs.";

        [TestMethod]
        public void TestGzip()
        {
            var inputData = Encoding.UTF8.GetBytes(OriginalText);
            var compressedData = GZipHelper.Compress(inputData);
            var decompressedData = GZipHelper.Decompress(compressedData);

            Assert.IsTrue(inputData.Count() > 0);
            Assert.IsTrue(decompressedData.Count() > 0);
            Assert.IsTrue(inputData.SequenceEqual(decompressedData));

            Console.WriteLine(@"Compressed size: {0:F2}%", 100 * ((double)compressedData.Length / decompressedData.Length));
            Console.WriteLine(Encoding.UTF8.GetString(decompressedData));
        }

        [TestMethod]
        public void Test7Zip()
        {
            var inputData = Encoding.UTF8.GetBytes(OriginalText);
            var compressedData = SevenZipHelper.Compress(inputData);
            var decompressedData = SevenZipHelper.Decompress(compressedData);
            var decompressedText = Encoding.UTF8.GetString(decompressedData);

            Assert.IsTrue(inputData.Count() > 0);
            Assert.IsTrue(decompressedData.Count() > 0);
            Assert.IsTrue(inputData.SequenceEqual(decompressedData));
            Assert.IsTrue(decompressedText.Equals(OriginalText));

            var msg = $"Compressed size: {100 * ((double)compressedData.Length / decompressedData.Length):F2}%";
            Console.WriteLine(msg);
            Console.WriteLine(Encoding.UTF8.GetString(decompressedData));
        }
    }
}
