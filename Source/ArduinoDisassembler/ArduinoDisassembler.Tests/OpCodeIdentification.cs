using ArduinoDisassembler.InstructionSet.OpCodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArduinoDisassembler.Tests
{
    [TestClass]
    public class OpCodeIdentification
    {
        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADCOpCode()
        {
            var bytes = new byte[] { 0b0001_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is ADC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADDOpCode()
        {
            var bytes = new byte[] { 0b0000_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is ADD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADIWOpCode()
        {
            var bytes = new byte[] { 0b1001_0110, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is ADIW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDOpCode()
        {
            var bytes = new byte[] { 0b0010_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is AND);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDIOpCode()
        {
            var bytes = new byte[] { 0b0111_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is ANDI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesASROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is ASR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesJMPOpCode()
        {
            var bytes = new byte[] {0b1001_0100, 0b0000_1100, 0b0000_0000, 0b1111_1111};
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _32BitOpCode);
            Assert.IsTrue(opcode is JMP);
        }
    }
}
