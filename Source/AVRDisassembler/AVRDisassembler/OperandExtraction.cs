using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using AVRDisassembler.InstructionSet.Operands;

namespace AVRDisassembler
{
    public static class OperandExtraction
    {
        public static IEnumerable<IOperand> ExtractOperands(Type type, byte[] bytes)
        {
            #region Instructions

            if (new[]
            {
                typeof(ADC),   typeof(ADD),   typeof(AND),   typeof(CP),
                typeof(CPC)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------rd ddddrrrr");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield break;
            }

            if (new[] { typeof(ADIW) }.Contains(type))
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

            if (new[] { typeof(ANDI), typeof(CBR) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("----KKKK ddddKKKK");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield return new Operand(OperandType.ConstantData, vals['K']);
                yield break;
            }

            if (new[] { typeof(ASR), typeof(COM) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
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

            if (new[] { typeof(BRBC), typeof(BRBS) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkksss");
                yield return new Operand(OperandType.StatusRegisterBit, vals['s']);
                yield return new Operand(OperandType.ConstantAddress, CalculateTwosComplement(vals['k'], 7));
                yield break;
            }

            if (new[]
            {
                typeof(BRCC),  typeof(BRCS),  typeof(BREQ),  typeof(BRGE),
                typeof(BRHC),  typeof(BRHS),  typeof(BRID),  typeof(BRIE),
                typeof(BRLO),  typeof(BRLT),  typeof(BRMI),  typeof(BRNE),
                typeof(BRPL),  typeof(BRSH),  typeof(BRTC),  typeof(BRTS),
                typeof(BRVC),  typeof(BRVS)
            }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkk---");
                yield return new Operand(OperandType.ConstantAddress, CalculateTwosComplement(vals['k'], 7));
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

            if (new[] { typeof(CBI) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- AAAAAbbb");
                yield return new Operand(OperandType.IOLocation, vals['A']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }

            if (new[] { typeof(CLR) }.Contains(type))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------dd dddddddd");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield break;
            }

            if (new[]
            {
                typeof(BREAK), typeof(CLC),   typeof(CLH),   typeof(CLI),
                typeof(CLN),   typeof(CLS),   typeof(CLT),   typeof(CLV),
                typeof(CLZ)
            }.Contains(type))
            {
                // No operands
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
