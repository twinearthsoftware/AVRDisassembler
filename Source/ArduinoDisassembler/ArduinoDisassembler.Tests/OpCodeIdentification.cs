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
        public void DisassemblerCorrectlyIdentifiesBLDOpCode()
        {
            var bytes = new byte[] { 0b1111_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BLD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREAKOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BREAK);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREQOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BREQ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRGEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRGE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRHC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRHS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIDOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRID);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIEOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRIE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLOOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRLO);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLTOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRMIOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRMI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRNEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRNE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRPLOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRPL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRSHOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRTC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRTS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRVC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BRVS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BSET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSTOpCode()
        {
            var bytes = new byte[] { 0b1111_1010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is BST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCBIOpCode()
        {
            var bytes = new byte[] { 0b1001_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLCOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLHOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1101_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLIOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1111_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLNOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1010_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLROpCode()
        {
            var bytes = new byte[] { 0b0010_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLSOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1100_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLTOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1110_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLVOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1011_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLZOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is CLZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCOMOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode is _16BitOpCode);
            Assert.IsTrue(opcode is COM);
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
