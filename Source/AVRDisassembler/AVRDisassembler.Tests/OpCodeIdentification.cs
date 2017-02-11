using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.DataTransfer;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVRDisassembler.Tests
{
    [TestClass]
    public class OpCodeIdentification
    {
        public static IEnumerable<IOpCode> Identify(byte[] bytes)
        {
            return AVRDisassembler.OpCodeIdentification.IdentifyOpCode(bytes);
        }

        public static void AssertSingleOpCode(
            byte[] bytes, 
            Func<IOpCode,bool> typeCheck, 
            OpCodeSize size = OpCodeSize._16)
        {
            var opcodes = Identify(bytes).ToList();
            Assert.IsTrue(opcodes.Count == 1);
            var opcode = opcodes.First();
            Assert.IsTrue(opcode.Size == size);
            Assert.IsTrue(typeCheck(opcode));
        }

        public static void AssertDoubleOpCode(
            byte[] bytes, 
            Func<IOpCode, bool> typeCheck1, 
            Func<IOpCode, bool> typeCheck2,
            OpCodeSize size = OpCodeSize._16)
        {
            var opcodes = Identify(bytes).ToList();
            Assert.IsTrue(opcodes.Count == 2);
            var opcode1 = opcodes.Single(typeCheck1);
            var opcode2 = opcodes.Single(typeCheck2);
            Assert.IsTrue(opcode1.Size == size);
            Assert.IsTrue(opcode2.Size == size);
        }

        public static void AssertTripleOpCode(
            byte[] bytes,
            Func<IOpCode, bool> typeCheck1, 
            Func<IOpCode, bool> typeCheck2, 
            Func<IOpCode, bool> typeCheck3,
            OpCodeSize size = OpCodeSize._16)
        {
            var opcodes = Identify(bytes).ToList();
            Assert.IsTrue(opcodes.Count == 3);
            var opcode1 = opcodes.Single(typeCheck1);
            var opcode2 = opcodes.Single(typeCheck2);
            var opcode3 = opcodes.Single(typeCheck3);
            Assert.IsTrue(opcode1.Size == size);
            Assert.IsTrue(opcode2.Size == size);
            Assert.IsTrue(opcode3.Size == size);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADCOpCode()
        {
            var bytes = new byte[] { 0b0001_1100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ADC, x => x is ROL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADDOpCode()
        {
            var bytes = new byte[] { 0b0000_1100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ADD, x => x is LSL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesADIWOpCode()
        {
            var bytes = new byte[] { 0b1001_0110, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is ADIW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDOpCode()
        {
            var bytes = new byte[] { 0b0010_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is AND, x => x is TST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesANDIOpCode()
        {
            var bytes = new byte[] { 0b0111_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ANDI, x => x is CBR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesASROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0101 };
            AssertSingleOpCode(bytes, x => x is ASR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode1()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1000_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode2()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1001_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode3()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1011_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode4()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1101_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode5()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1111_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode6()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1110_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode7()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1010_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBCLROpCode8()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1100_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBLDOpCode()
        {
            var bytes = new byte[] { 0b1111_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is BLD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0000 };
            AssertTripleOpCode(bytes, 
                x => x is BRBC, x => x is BRCC, x => x is BRSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode2()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0001 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRNE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode3()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0010 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRPL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode4()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0011 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRVC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode5()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0100 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRGE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode6()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0101 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRHC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode7()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0110 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRTC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBCOpCode8()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0111 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRID);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0000 };
            AssertTripleOpCode(bytes,
                x => x is BRBS, x => x is BRCS, x => x is BRLO);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode2()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0001 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BREQ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode3()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0010 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRMI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode4()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0011 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRVS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode5()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0100 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode6()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0101 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRHS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode7()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0110 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRTS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRBSOpCode8()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0111 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRIE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRCCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0000 };
            AssertTripleOpCode(bytes,
                x => x is BRBC, x => x is BRCC, x => x is BRSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRCSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0000 };
            AssertTripleOpCode(bytes,
                x => x is BRBS, x => x is BRCS, x => x is BRLO);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREAKOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1001_1000 };
            AssertSingleOpCode(bytes, x => x is BREAK);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBREQOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0001 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BREQ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRGEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0100 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRGE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0101 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRHC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRHSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0101 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRHS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIDOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0111 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRID);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRIEOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0111 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRIE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLOOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0000 };
            AssertTripleOpCode(bytes,
                x => x is BRBS, x => x is BRCS, x => x is BRLO);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRLTOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0100 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRMIOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0010 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRMI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRNEOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0001 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRNE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRPLOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0010 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRPL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRSHOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0000 };
            AssertTripleOpCode(bytes,
                x => x is BRBC, x => x is BRCC, x => x is BRSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0110 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRTC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRTSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0110 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRTS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVCOpCode()
        {
            var bytes = new byte[] { 0b1111_0100, 0b0000_0011 };
            AssertDoubleOpCode(bytes, x => x is BRBC, x => x is BRVC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBRVSOpCode()
        {
            var bytes = new byte[] { 0b1111_0000, 0b0000_0011 };
            AssertDoubleOpCode(bytes, x => x is BRBS, x => x is BRVS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode1()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode2()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0001_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode3()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0010_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode4()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0011_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode5()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0100_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SES);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode6()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0110_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode7()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0101_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSETOpCode8()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0111_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesBSTOpCode()
        {
            var bytes = new byte[] { 0b1111_1010, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is BST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1110 };
            AssertSingleOpCode(bytes, x => x is CALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCBIOpCode()
        {
            var bytes = new byte[] { 0b1001_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is CBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCBROpCode()
        {
            var bytes = new byte[] { 0b0111_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ANDI, x => x is CBR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLCOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1000_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLHOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1101_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLIOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1111_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLNOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1010_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLROpCode()
        {
            var bytes = new byte[] { 0b0010_0100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is CLR, x => x is EOR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLSOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1100_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLTOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1110_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLVOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1011_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCLZOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b1001_1000 };
            AssertDoubleOpCode(bytes, x => x is BCLR, x => x is CLZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCOMOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is COM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPOpCode()
        {
            var bytes = new byte[] { 0b0001_0100, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is CP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPCOpCode()
        {
            var bytes = new byte[] { 0b0000_0100, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is CPC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPIOpCode()
        {
            var bytes = new byte[] { 0b0011_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is CPI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesCPSEOpCode()
        {
            var bytes = new byte[] { 0b0001_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is CPSE);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesDECOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1010 };
            AssertSingleOpCode(bytes, x => x is DEC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesDESOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1011 };
            AssertSingleOpCode(bytes, x => x is DES);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesEICALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0001_1001 };
            AssertSingleOpCode(bytes, x => x is EICALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesEIJMPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0001_1001 };
            AssertSingleOpCode(bytes, x => x is EIJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1101_1000 };
            AssertSingleOpCode(bytes, x => x is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0110 };
            AssertSingleOpCode(bytes, x => x is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesELPMOpCode3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0111 };
            AssertSingleOpCode(bytes, x => x is ELPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesEOROpCode()
        {
            var bytes = new byte[] { 0b0010_0100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is CLR, x => x is EOR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is FMUL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULSOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b1000_0000 };
            AssertSingleOpCode(bytes, x => x is FMULS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesFMULSUOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b1000_1000 };
            AssertSingleOpCode(bytes, x => x is FMULSU);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesICALLOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0000_1001 };
            AssertSingleOpCode(bytes, x => x is ICALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesIJMPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1001 };
            AssertSingleOpCode(bytes, x => x is IJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesINOpCode()
        {
            var bytes = new byte[] { 0b1011_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is IN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesINCOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0011 };
            AssertSingleOpCode(bytes, x => x is INC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesJMPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1100, 0b0000_0000, 0b1111_1111 };
            AssertSingleOpCode(bytes, x => x is JMP, OpCodeSize._32);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLACOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0110 };
            AssertSingleOpCode(bytes, x => x is LAC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLASOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0101 };
            AssertSingleOpCode(bytes, x => x is LAS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLATOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0111 };
            AssertSingleOpCode(bytes, x => x is LAT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX1()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1100 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1101 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeX3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1110 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY1()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1001 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1010 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeY4()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ1()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0001 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0010 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDOpCodeZ4()
        {
            var bytes = new byte[] { 0b1000_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is LD);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDIOpCode1()
        {
            var bytes = new byte[] { 0b1110_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is LDI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDIOpCode2()
        {
            var bytes = new byte[] { 0b1110_1111, 0b0000_1111 };
            AssertDoubleOpCode(bytes, x => x is LDI, x => x is SER);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDS32OpCode()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0000, 0b0000_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is LDS32, OpCodeSize._32);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLDS16OpCode()
        {
            var bytes = new byte[] { 0b1010_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is LDS16);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1100_1000 };
            AssertSingleOpCode(bytes, x => x is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0100 };
            AssertSingleOpCode(bytes, x => x is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLPMOpCode3()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_0101 };
            AssertSingleOpCode(bytes, x => x is LPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLSLOpCode()
        {
            var bytes = new byte[] { 0b0000_1100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ADD, x => x is LSL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesLSROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0110 };
            AssertSingleOpCode(bytes, x => x is LSR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMOVOpCode()
        {
            var bytes = new byte[] { 0b0010_1100, 0b0000_0110 };
            AssertSingleOpCode(bytes, x => x is MOV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMOVWOpCode()
        {
            var bytes = new byte[] { 0b0000_0001, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is MOVW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULOpCode()
        {
            var bytes = new byte[] { 0b1001_1100, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is MUL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULSOpCode()
        {
            var bytes = new byte[] { 0b0000_0010, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is MULS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesMULSUOpCode()
        {
            var bytes = new byte[] { 0b0000_0011, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is MULSU);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesNEGOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0001 };
            AssertSingleOpCode(bytes, x => x is NEG);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesNOPOpCode()
        {
            var bytes = new byte[] { 0b0000_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is NOP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesOROpCode()
        {
            var bytes = new byte[] { 0b0010_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is OR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesORIOpCode()
        {
            var bytes = new byte[] { 0b0110_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ORI, x => x is SBR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesOUTOpCode()
        {
            var bytes = new byte[] { 0b1011_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is OUT);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesPOPOpCode()
        {
            var bytes = new byte[] { 0b1001_0000, 0b0000_1111 };
            AssertSingleOpCode(bytes, x => x is POP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesPUSHOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1111 };
            AssertSingleOpCode(bytes, x => x is PUSH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRCALLOpCode()
        {
            var bytes = new byte[] { 0b1101_0000, 0b0000_1111 };
            AssertSingleOpCode(bytes, x => x is RCALL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRETOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is RET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRETIOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b0001_1000 };
            AssertSingleOpCode(bytes, x => x is RETI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesRJMPOpCode()
        {
            var bytes = new byte[] { 0b1100_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is RJMP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesROLOpCode()
        {
            var bytes = new byte[] { 0b0001_1100, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ADC, x => x is ROL);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesROROpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0111 };
            AssertSingleOpCode(bytes, x => x is ROR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBCOpCode()
        {
            var bytes = new byte[] { 0b0000_1000, 0b0000_0111 };
            AssertSingleOpCode(bytes, x => x is SBC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBCIOpCode()
        {
            var bytes = new byte[] { 0b0100_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBCI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBIOpCode()
        {
            var bytes = new byte[] { 0b1001_1010, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBICOpCode()
        {
            var bytes = new byte[] { 0b1001_1001, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBIC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBISOpCode()
        {
            var bytes = new byte[] { 0b1001_1011, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBIS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBIWOpCode()
        {
            var bytes = new byte[] { 0b1001_0111, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBIW);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBROpCode()
        {
            var bytes = new byte[] { 0b0110_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is ORI, x => x is SBR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBRCOpCode()
        {
            var bytes = new byte[] { 0b1111_1100, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBRC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSBRSOpCode()
        {
            var bytes = new byte[] { 0b1111_1110, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SBRS);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSECOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEC);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEHOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0101_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEH);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEIOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0111_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSENOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0010_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEN);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEROpCode()
        {
            var bytes = new byte[] { 0b1110_1111, 0b0000_1111 };
            AssertDoubleOpCode(bytes, x => x is LDI, x => x is SER);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSESOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0100_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SES);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSETOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0110_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SET);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEVOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0011_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEV);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSEZOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0001_1000 };
            AssertDoubleOpCode(bytes, x => x is BSET, x => x is SEZ);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSLEEPOpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1000_1000 };
            AssertSingleOpCode(bytes, x => x is SLEEP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSPMOpCode1()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1110_1000 };
            AssertSingleOpCode(bytes, x => x is SPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSPMOpCode2()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1111_1000 };
            AssertSingleOpCode(bytes, x => x is SPM);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX1()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1100 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1101 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeX3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1110 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY1()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1001 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_1010 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeY4()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_1000 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ1()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ2()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0001 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ3()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0010 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTOpCodeZ4()
        {
            var bytes = new byte[] { 0b1000_0010, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is ST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTS32OpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0000, 0b0000_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is STS32, OpCodeSize._32);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSTS16OpCode()
        {
            var bytes = new byte[] { 0b1010_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is STS16);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSUBOpCode()
        {
            var bytes = new byte[] { 0b0001_1000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SUB);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSUBIOpCode()
        {
            var bytes = new byte[] { 0b0101_0000, 0b0000_0000 };
            AssertSingleOpCode(bytes, x => x is SUBI);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesSWAPOpCode()
        {
            var bytes = new byte[] { 0b1001_0100, 0b0000_0010 };
            AssertSingleOpCode(bytes, x => x is SWAP);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesTSTOpCode()
        {
            var bytes = new byte[] { 0b0010_0000, 0b0000_0000 };
            AssertDoubleOpCode(bytes, x => x is AND, x => x is TST);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesWDROpCode()
        {
            var bytes = new byte[] { 0b1001_0101, 0b1010_1000 };
            AssertSingleOpCode(bytes, x => x is WDR);
        }

        [TestMethod]
        public void DisassemblerCorrectlyIdentifiesXCHOpCode()
        {
            var bytes = new byte[] { 0b1001_0010, 0b0000_0100 };
            AssertSingleOpCode(bytes, x => x is XCH);
        }
    }
}