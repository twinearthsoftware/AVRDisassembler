using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVRDisassembler.Tests
{
    [TestClass]
    public class Extensions
    {

        private static readonly Random _random = new Random();

        #region MapToMask - Valid Masks

        [TestMethod]
        public void MapToMaskAcceptsValidMaskSingleByteSinglePart()
        {
            var r = _random.Next(0, 255);
            var result = new[] { (byte)r }.MapToMask("aaaaaaaa");
            Assert.IsTrue(result.Keys.Count == 1);
            Assert.IsTrue(result['a'] == r);
        }

        [TestMethod]
        public void MapToMaskAcceptsValidMaskSingleByteTwoParts()
        {
            var r1 = _random.Next(0, 255);
            var result = new[] { (byte)r1 }.MapToMask("aaaaaaab");
            Assert.IsTrue(result.Keys.Count == 2);
            Assert.IsTrue(result['a'] == r1 >> 1);
            Assert.IsTrue(result['b'] == (r1 & 0x01));
        }

        [TestMethod]
        public void MapToMaskAcceptsValidMaskSingleByteCombinedTwoParts()
        {
            var r1 = _random.Next(0, 255);
            var result = new[] { (byte)r1 }.MapToMask("bbbaaaab");
            Assert.IsTrue(result.Keys.Count == 2);
            Assert.IsTrue(result['a'] == ((r1 >> 1) & 0x0f));
            Assert.IsTrue(result['b'] == (r1 & 0x01) + ((r1 >> 4) & 0xfe));
        }

        [TestMethod]
        public void MapToMaskAcceptsValidMaskMultipleBytesMultipleParts()
        {
            var r1 = _random.Next(0, 255);
            var r2 = _random.Next(0, 255);
            var result = new[] { (byte)r1, (byte)r2 }.MapToMask("aaaaaaaa bbbbbbbb");
            Assert.IsTrue(result.Keys.Count == 2);
            Assert.IsTrue(result['a'] == r1);
            Assert.IsTrue(result['b'] == r2);
        }

        [TestMethod]
        public void MapToMaskAcceptsValidMaskMultipleBytesSinglePart()
        {
            var r1 = _random.Next(0, 255);
            var r2 = _random.Next(0, 255);
            var result = new[] { (byte)r1, (byte)r2 }.MapToMask("aaaaaaaa aaaaaaaa");
            Assert.IsTrue(result.Keys.Count == 1);
            Assert.IsTrue(result['a'] == (r1 << 8) + r2);
        }

        #endregion

        #region MapToMask - Invalid Masks

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskEmptyMask()
        {
            new byte[] { 0b1111_0000, 0b0000_1111 }.MapToMask("");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskNullMask()
        {
            new byte[] { 0b1111_0000, 0b0000_1111 }.MapToMask(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskNoSpaceDelimiters()
        {
            new byte[] {0b1111_0000, 0b0000_1111}.MapToMask("aaaaaaaabbbbbbbb");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskWrongNumberOfMaskparts()
        {
            new byte[] { 0b1111_0000, 0b0000_1111 }.MapToMask("aaaaaaaa bbbbbbbb cccccccc");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskTooShortMaskPart()
        {
            new byte[] { 0b0000_0000 }.MapToMask("aaaaaaa");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskTooLongMaskPart()
        {
            new byte[] { 0b0000_0000 }.MapToMask("aaaaaaaaa");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskTooShortMaskPartMultipleBytes()
        {
            new byte[] { 0b0000_0000 }.MapToMask("aaaaaaaa bbb");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskTooLongMaskPartMultipleBytes()
        {
            new byte[] { 0b0000_0000 }.MapToMask("aaaaaaaaa bbbbbbbb");
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void MapToMaskThrowsExceptionOnInvalidMaskInsufficientBytes()
        {
            new byte[] { 0b0000_0000 }.MapToMask("aaaaaaaa bbbbbbbb");
        }

        #endregion
    }
}