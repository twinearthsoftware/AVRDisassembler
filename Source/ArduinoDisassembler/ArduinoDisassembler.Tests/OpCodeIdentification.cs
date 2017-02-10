using ArduinoDisassembler.InstructionSet.OpCodes;
using ArduinoDisassembler.InstructionSet.OpCodes.Arithmetic;
using ArduinoDisassembler.InstructionSet.OpCodes.Bits;
using ArduinoDisassembler.InstructionSet.OpCodes.Branch;
using ArduinoDisassembler.InstructionSet.OpCodes.DataTransfer;
using ArduinoDisassembler.InstructionSet.OpCodes.MCUControl;
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
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ADC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADDOpCode()
        {
            var bytes = new byte[] { 0b0000_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ADD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADIWOpCode()
        {
            var bytes = new byte[] { 0b1001_0110, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ADIW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDOpCode()
        {
            var bytes = new byte[] { 0b0010_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is AND);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDIOpCode()
        {
            var bytes = new byte[] { 0b0111_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ANDI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesASROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ASR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBLDOpCode()
        {
            var bytes = new byte[] { 0b1111_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BLD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREAKOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BREAK);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREQOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BREQ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRGEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRGE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRHC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRHS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIDOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRID);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIEOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRIE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLOOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRLO);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLTOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRMIOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRMI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRNEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRNE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRPLOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRPL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRSHOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRTC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRTS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRVC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BRVS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BSET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSTOpCode()
        {
            var bytes = new byte[] { 0b1111_1010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is BST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCBIOpCode()
        {
            var bytes = new byte[] { 0b1001_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLCOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLHOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1101_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLIOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1111_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLNOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1010_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLROpCode()
        {
            var bytes = new byte[] { 0b0010_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLSOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1100_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLTOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1110_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLVOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1011_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLZOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CLZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCOMOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is COM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPOpCode()
        {
            var bytes = new byte[] { 0b0001_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPCOpCode()
        {
            var bytes = new byte[] { 0b0000_0100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CPC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPIOpCode()
        {
            var bytes = new byte[] { 0b0011_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CPI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPSEOpCode()
        {
            var bytes = new byte[] { 0b0001_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is CPSE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesDECOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is DEC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesDESOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is DES);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesEICALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0001_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is EICALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesEIJMPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0001_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is EIJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1101_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is FMUL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULSOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b1000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is FMULS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULSUOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b1000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is FMULSU);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesICALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0000_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ICALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesIJMPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is IJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesINOpCode()
        {
            var bytes = new byte[] { 0b1011_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is IJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesINCOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0011 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is INC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesJMPOpCode()
        {
            var bytes = new byte[] {0b1001_0100, 0b0000_1100, 0b0000_0000, 0b1111_1111};
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._32);
            Assert.IsTrue(opcode is JMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLACOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LAC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLASOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LAS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLATOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LAT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX1()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY1()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY4()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ1()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ4()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDIOpCode()
        {
            var bytes = new byte[] { 0b1110_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LDI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDS32OpCode()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0000, 0b0000_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._32);
            Assert.IsTrue(opcode is LDS32);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDS16OpCode()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LDS16);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1100_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLSLOpCode()
        {
            var bytes = new byte[] { 0b0000_1100, 0b0000_0101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LSL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLSROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is LSR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMOVOpCode()
        {
            var bytes = new byte[] { 0b0010_1100, 0b0000_0110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is MOV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMOVWOpCode()
        {
            var bytes = new byte[] { 0b0000_0001, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is MOVW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULOpCode()
        {
            var bytes = new byte[] { 0b1001_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is MUL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULSOpCode()
        {
            var bytes = new byte[] { 0b0000_0010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is MULS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULSUOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is MULSU);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesNEGOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is NEG);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesNOPOpCode()
        {
            var bytes = new byte[] { 0b0000_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is NOP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesOROpCode()
        {
            var bytes = new byte[] { 0b0010_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is OR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesORIOpCode()
        {
            var bytes = new byte[] { 0b0110_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ORI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesOUTOpCode()
        {
            var bytes = new byte[] { 0b1011_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is OUT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesPOPOpCode()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is POP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesPUSHOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is PUSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRCALLOpCode()
        {
            var bytes = new byte[] { 0b1101_0000, 0b0000_1111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is RCALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRETOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is RET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRETIOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is RETI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRJMPOpCode()
        {
            var bytes = new byte[] { 0b1100_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is RJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesROLOpCode()
        {
            var bytes = new byte[] { 0b0001_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ROL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesROROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ROR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBCOpCode()
        {
            var bytes = new byte[] { 0b0000_1000, 0b0000_0111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBCIOpCode()
        {
            var bytes = new byte[] { 0b0100_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBCI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBIOpCode()
        {
            var bytes = new byte[] { 0b1001_1010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBICOpCode()
        {
            var bytes = new byte[] { 0b1001_1001, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBIC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBISOpCode()
        {
            var bytes = new byte[] { 0b1001_1011, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBIS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBIWOpCode()
        {
            var bytes = new byte[] { 0b1001_0111, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBIW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBROpCode()
        {
            var bytes = new byte[] { 0b0110_0111, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBRCOpCode()
        {
            var bytes = new byte[] { 0b1111_1100, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBRC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBRSOpCode()
        {
            var bytes = new byte[] { 0b1111_1110, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SBRS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSECOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEHOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEIOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0111_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSENOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0010_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEROpCode()
        {
            var bytes = new byte[] { 0b1110_1111, 0b0000_1111 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SER);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSESOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0100_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SES);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSETOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0110_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEVOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0011_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEZOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0001_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SEZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSLEEPOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SLEEP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1110_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1110_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX1()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1101 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1110 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY1()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY4()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ1()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0001 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ4()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTS32OpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0000, 0b0000_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._32);
            Assert.IsTrue(opcode is STS32);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTS16OpCode()
        {
            var bytes = new byte[] { 0b1010_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is STS16);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSUBOpCode()
        {
            var bytes = new byte[] { 0b0001_1000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SUB);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSUBIOpCode()
        {
            var bytes = new byte[] { 0b0101_0000, 0b0000_0000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SUBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSWAPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is SWAP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesTSTOpCode()
        {
            var bytes = new byte[] { 0b0010_0000, 0b0000_0010 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is TST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesWDROpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1010_1000 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is WDR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesXCHOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0100 };
            var opcode = Disassembler.IdentifyOpCode(bytes);
            Assert.IsTrue(opcode.Size == OpCodeSize._16);
            Assert.IsTrue(opcode is XCH);
        }
    }
}
