using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.DataTransfer;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using AVRDisassembler.InstructionSet.Operands;

namespace AVRDisassembler
{
    // TODO Implement LD Y4 / LD Z4 / ST Y4 / ST Z4
    public static class OperandExtraction
    {
        public static IEnumerable<IOperand> ExtractOperands(Type type, byte[] bytes)
        {
            #region Instructions

            if (new[]
            {
                typeof(ADC),    typeof(ADD),    typeof(AND),    typeof(CP),
                typeof(CPC),    typeof(CPSE),   typeof(EOR),    typeof(MOV),
                typeof(MUL),    typeof(OR),     typeof(SBC)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------rd ddddrrrr");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield break;
            }

            if (new[] { typeof(ADIW), typeof(SBIW) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- KKddKKKK");
                var r = 0;
                switch (vals['d'])
                {
                    case 0: r = 24; break;
                    case 1: r = 26; break;
                    case 2: r = 28; break;
                    case 3: r = 30; break;
                }
                yield return new Operand(OperandType.DestinationRegister, r);
                yield return new Operand(OperandType.ConstantData, vals['K']);
                yield break;
            }

            if (new[]
            {
                typeof(ANDI),   typeof(CBR),    typeof(CPI),    typeof(LDI),
                typeof(ORI),    typeof(SBCI),   typeof(SBR)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("----KKKK ddddKKKK");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield return new Operand(OperandType.ConstantData, vals['K']);
                yield break;
            }

            if (new[]
            {
                typeof(ASR),    typeof(COM),    typeof(DEC),    typeof(INC),
                typeof(LSR),    typeof(NEG),    typeof(POP),    typeof(ROR)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield break;
            }

            if (new[] { typeof(SER) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- dddd----");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield break;
            }

            if (new[] { typeof(PUSH) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------r rrrr----");
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield break;
            }

            if (new[] { typeof(LAC), typeof(LAS), typeof(LAT) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                yield return new Operand(OperandType._ZRegister, false, false);
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield break;
            }

            if (new[] { typeof(BCLR), typeof(BSET) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- -sss----");
                yield return new Operand(OperandType.StatusRegisterBit, vals['s']);
                yield break;
            }

            if (new[] { typeof(BLD), typeof(BST) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd-bbb");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }

            if (new[] { typeof(SBRC), typeof(SBRS) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------r rrrr-bbb");
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }

            if (new[] {typeof(MOVW)}.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- ddddrrrr");
                yield return new Operand(OperandType.DestinationRegister, vals['d'] << 1);
                yield return new Operand(OperandType.SourceRegister, vals['r'] << 1);
                yield break;
            }

            if (new[] { typeof(BRBC), typeof(BRBS) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkksss");
                yield return new Operand(OperandType.StatusRegisterBit, vals['s']);
                yield return new Operand(
                    OperandType.ConstantAddress, 
                    CalculateTwosComplement(vals['k'], 7) * 2)
                    { RepresentationMode = RepresentationMode.RelativeOffset };
                yield break;
            }

            if (new[]
            {
                typeof(BRCC),   typeof(BRCS),   typeof(BREQ),   typeof(BRGE),
                typeof(BRHC),   typeof(BRHS),   typeof(BRID),   typeof(BRIE),
                typeof(BRLO),   typeof(BRLT),   typeof(BRMI),   typeof(BRNE),
                typeof(BRPL),   typeof(BRSH),   typeof(BRTC),   typeof(BRTS),
                typeof(BRVC),   typeof(BRVS)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkk---");
                yield return new Operand(
                    OperandType.ConstantAddress, 
                    CalculateTwosComplement(vals['k'], 7) * 2)
                    { RepresentationMode = RepresentationMode.RelativeOffset };

                yield break;
            }

            if (new[] { typeof(LDS32) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1], bytes[2], bytes[3] }
                    .MapToMask("-------d dddd---- kkkkkkkk kkkkkkkk");

                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(
                    OperandType.ConstantAddress, 
                    new[] { bytes[2], bytes[3] })
                    { RepresentationMode = RepresentationMode.FullBytes };
                yield break;
            }

            if (new[] {typeof(LDS16)}.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-----kkk ddddkkkk");

                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.ConstantAddress, vals['k']);
                yield break;
            }

            if (new[] { typeof(CALL), typeof(JMP) }.Contains(type))
            {
                // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are 
                // always on even boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                var vals = new[] { bytes[0], bytes[1], bytes[2], bytes[3] }
                    .MapToMask("-------k kkkk---k kkkkkkkk kkkkkkkk");

                var addressVal = vals['k'] << 1;
                yield return new Operand(OperandType.ConstantAddress, addressVal);
                yield break;
            }

            if (new[] { typeof(RCALL), typeof(RJMP) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("----kkkk kkkkkkkk");
                var baseVal = vals['k'];
                if (type == typeof(RJMP)) baseVal = CalculateTwosComplement(baseVal, 12);
                yield return new Operand(OperandType.ConstantAddress, baseVal  * 2)
                { RepresentationMode = RepresentationMode.RelativeOffset };
                yield break;
            }

            if (new[] { typeof(CBI), typeof(SBI), typeof(SBIC), typeof(SBIS) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- AAAAAbbb");
                yield return new Operand(OperandType.IOLocation, vals['A']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }

            if (new[] { typeof(CLR) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- KKKK----");
                yield return new Operand(OperandType.ConstantData, vals['K']);
                yield break;
            }

            if (new[] { typeof(DES), typeof(LSL), typeof(ROL) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------dd dddddddd");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield break;
            }

            if (new[] { typeof(FMUL), typeof(FMULS), typeof(FMULSU), typeof(MULSU) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- -ddd-rrr");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield return new Operand(OperandType.SourceRegister, 16 + vals['r']);
                yield break;
            }

            if (new[] { typeof(MULS) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- ddddrrrr");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield return new Operand(OperandType.SourceRegister, 16 + vals['r']);
                yield break;
            }

            if (new[] { typeof(IN) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-----AAd ddddAAAA");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.IOLocation, vals['A']);
                yield break;
            }

            if (new[] { typeof(OUT) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-----AAr rrrrAAAA");
                yield return new Operand(OperandType.IOLocation, vals['A']);
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield break;
            }

            if (new[]
            {
                typeof(BREAK),  typeof(CLC),    typeof(CLH),    typeof(CLI),
                typeof(CLN),    typeof(CLS),    typeof(CLT),    typeof(CLV),
                typeof(CLZ),    typeof(EICALL), typeof(EIJMP),  typeof(ICALL),
                typeof(IJMP),   typeof(NOP),    typeof(RET),    typeof(RETI),
                typeof(SEC),    typeof(SEH),    typeof(SEI),    typeof(SEN),
                typeof(SES),    typeof(SET),    typeof(SEV),    typeof(SEZ),
                typeof(SLEEP)
            }.Contains(type))
            {
                // No operands
                yield break;
            }

            // Instructions that potentially match more than one opcode:
            if (type == typeof(ELPM))
            {
                if (bytes[0] == 0b1001_0101 && bytes[2] == 0b1101_1000)
                {
                    yield break; // (1) no operands
                }
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType._ZRegister, (bytes[1] & 0x01) == 0b1, false); // (2) - (3)
                yield break;
            }

            if (type == typeof(LD))
            {
                var b = bytes[0] >> 1;
                if (b == 0b100_1000 || b == 0b100_0000) // X, Y, Z
                {
                    var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                    yield return new Operand(OperandType.DestinationRegister, vals['d']);
                    switch (bytes[1] & 0x0f)
                    {
                        case 0b0000: yield return new Operand(OperandType._ZRegister, false, false); break; // (Z1)
                        case 0b0001: yield return new Operand(OperandType._ZRegister, true, false); break;  // (Z2)
                        case 0b0010: yield return new Operand(OperandType._ZRegister, false, true); break;  // (Z3)
                        case 0b1000: yield return new Operand(OperandType._YRegister, false, false); break; // (Y1)
                        case 0b1001: yield return new Operand(OperandType._YRegister, true, false); break;  // (Y2)
                        case 0b1010: yield return new Operand(OperandType._YRegister, false, true); break;  // (Y3)
                        case 0b1100: yield return new Operand(OperandType._XRegister, false, false); break; // (X1)
                        case 0b1101: yield return new Operand(OperandType._XRegister, true, false); break;  // (X2)
                        case 0b1110: yield return new Operand(OperandType._XRegister, false, true); break;  // (X3)
                    }
                }
                yield break;
            }

            if (type == typeof(LPM))
            {
                var lastNibble = bytes[1] & 0x0f;
                if (lastNibble == 0b1000) yield break; // No Operands, 1
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType._ZRegister, lastNibble == 0b0101, false);
                yield break;
            }

            #endregion

            #region Pseudoinstructions

            if (new[] { typeof(DATA) }.Contains(type))
            {
                yield return new Operand(OperandType._RawData, bytes);
            }

            #endregion
        }

        public static int CalculateTwosComplement(int val, int numberOfBits)
        {
            var mask = 1 << numberOfBits - 1;
            return -(val & mask) + (val & ~mask);
        }
    }
}
