using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using AVRDisassembler.InstructionSet.Operands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVRDisassembler.Tests
{
    [TestClass]
    public class OperandExtraction
    {
        private static IEnumerable<IOperand> GetOperands(Type type, byte[] bytes)
        {
            return AVRDisassembler.OperandExtraction
                .ExtractOperands(type, bytes);
        }

        [TestMethod]
        public void OperandsForADCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(ADC),
                new byte[] {0b0001_1101, 0b0001_1000})
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var r = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 17);
            Assert.IsTrue(r.Type == OperandType.SourceRegister);
            Assert.IsTrue(r.Value == 8);
        }

        [TestMethod]
        public void OperandsForADDGetParsedCorrectly()
        {
            var results = GetOperands(typeof(ADD),
                new byte[] { 0b0000_1101, 0b0001_1100 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var r = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 17);
            Assert.IsTrue(r.Type == OperandType.SourceRegister);
            Assert.IsTrue(r.Value == 12);
        }

        [TestMethod]
        public void OperandsForADIWGetParsedCorrectly()
        {
            var results = GetOperands(typeof(ADIW),
                new byte[] { 0b1001_0110, 0b0001_1100 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var K = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 26);
            Assert.IsTrue(K.Type == OperandType.ConstantData);
            Assert.IsTrue(K.Value == 12);
        }

        [TestMethod]
        public void OperandsForANDGetParsedCorrectly()
        {
            var results = GetOperands(typeof(AND),
                new byte[] { 0b0010_0011, 0b1111_1111 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var r = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 31);
            Assert.IsTrue(r.Type == OperandType.SourceRegister);
            Assert.IsTrue(r.Value == 31);
        }

        [TestMethod]
        public void OperandsForANDIGetParsedCorrectly()
        {
            var results = GetOperands(typeof(ANDI),
                new byte[] { 0b0111_0011, 0b0011_1111 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var K = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 19);
            Assert.IsTrue(K.Type == OperandType.ConstantData);
            Assert.IsTrue(K.Value == 63);
        }

        [TestMethod]
        public void OperandsForASRGetParsedCorrectly()
        {
            var results = GetOperands(typeof(ASR),
                new byte[] { 0b1001_0100, 0b0011_0101 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var d = results[0];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 3);
        }

        [TestMethod]
        public void OperandsForBCLRGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BCLR),
                new byte[] { 0b1001_0100, 0b1111_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var s = results[0];
            Assert.IsTrue(s.Type == OperandType.StatusRegisterBit);
            Assert.IsTrue(s.Value == 7);
        }

        [TestMethod]
        public void OperandsForBLDGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BLD),
                new byte[] { 0b1111_1001, 0b1111_0101 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var b = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 31);
            Assert.IsTrue(b.Type == OperandType.BitRegisterIO);
            Assert.IsTrue(b.Value == 5);
        }

        [TestMethod]
        public void OperandsForBRBCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRBC),
                new byte[] { 0b1111_0100, 0b0001_1101 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var s = results[0];
            var k = results[1];
            Assert.IsTrue(s.Type == OperandType.StatusRegisterBit);
            Assert.IsTrue(s.Value == 5);
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBRBSGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRBS),
                new byte[] { 0b1111_0100, 0b0001_1101 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var s = results[0];
            var k = results[1];
            Assert.IsTrue(s.Type == OperandType.StatusRegisterBit);
            Assert.IsTrue(s.Value == 5);
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBRCCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRCC),
                new byte[] { 0b1111_0100, 0b0001_1101 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBRCSGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRCS),
                new byte[] { 0b1111_0000, 0b0001_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBREAKGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BREAK),
                new byte[] { 0b1001_0101, 0b1001_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 0);
        }

        [TestMethod]
        public void OperandsForBREQGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BREQ),
                new byte[] { 0b1111_0001, 0b1001_0001 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 100);
        }

        [TestMethod]
        public void OperandsForBRGEGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRGE),
                new byte[] { 0b1111_0101, 0b1001_0100 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 100);
        }

        [TestMethod]
        public void OperandsForBRHCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRHC),
                new byte[] { 0b1111_0101, 0b1001_0101 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 100);
        }

        [TestMethod]
        public void OperandsForBRHSGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRHS),
                new byte[] { 0b1111_0100, 0b1001_0101 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 36);
        }

        [TestMethod]
        public void OperandsForBRIDGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRID),
                new byte[] { 0b1111_0100, 0b1001_0111 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 36);
        }

        [TestMethod]
        public void OperandsForBRIEGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRIE),
                new byte[] { 0b1111_0000, 0b1001_0111 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 36);
        }

        [TestMethod]
        public void OperandsForBRLOGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRLO),
                new byte[] { 0b1111_0000, 0b0000_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 2);
        }

        [TestMethod]
        public void OperandsForBRLTGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRLT),
                new byte[] { 0b1111_0000, 0b0001_0100 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 4);
        }

        [TestMethod]
        public void OperandsForBRMIGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRMI),
                new byte[] { 0b1111_0000, 0b0001_1010 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBRNEGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRNE),
                new byte[] { 0b1111_0100, 0b0001_1001 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 6);
        }

        [TestMethod]
        public void OperandsForBRPLGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRPL),
                new byte[] { 0b1111_0100, 0b0011_1010 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 14);
        }

        [TestMethod]
        public void OperandsForBRSHGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRSH),
                new byte[] { 0b1111_0100, 0b0111_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 30);
        }

        [TestMethod]
        public void OperandsForBRTCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRTC),
                new byte[] { 0b1111_0101, 0b1111_1110 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 126);
        }

        [TestMethod]
        public void OperandsForBRTSGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRTS),
                new byte[] { 0b1111_0000, 0b0000_0110 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 0);
        }

        [TestMethod]
        public void OperandsForBRVCGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRVC),
                new byte[] { 0b1111_0100, 0b0000_1011 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 2);
        }

        [TestMethod]
        public void OperandsForBRVSGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BRVS),
                new byte[] { 0b1111_0011, 0b0101_0011 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == -44);
        }

        [TestMethod]
        public void OperandsForBSETGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BSET),
                new byte[] { 0b1001_0100, 0b0101_1000 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var s = results[0];
            Assert.IsTrue(s.Type == OperandType.StatusRegisterBit);
            Assert.IsTrue(s.Value == 5);
        }

        [TestMethod]
        public void OperandsForBSTGetParsedCorrectly()
        {
            var results = GetOperands(typeof(BST),
                new byte[] { 0b1111_1010, 0b0001_0001 })
                .ToList();

            Assert.IsTrue(results.Count == 2);
            var d = results[0];
            var b = results[1];
            Assert.IsTrue(d.Type == OperandType.DestinationRegister);
            Assert.IsTrue(d.Value == 1);
            Assert.IsTrue(b.Type == OperandType.BitRegisterIO);
            Assert.IsTrue(b.Value == 1);
        }

        [TestMethod]
        public void OperandsForCALLGetParsedCorrectly()
        {
            var results = GetOperands(typeof(CALL),
                new byte[] { 0b1001_0100, 0b0000_1110, 0b0000_0000, 0b0000_1111 })
                .ToList();

            Assert.IsTrue(results.Count == 1);
            var k = results[0];
            Assert.IsTrue(k.Type == OperandType.ConstantAddress);
            Assert.IsTrue(k.Value == 30);
        }
    }
}
