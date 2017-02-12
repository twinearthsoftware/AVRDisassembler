using System;
using System.Collections.Generic;
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

            if (   type == typeof(ADC)
                || type == typeof(ADD)
                || type == typeof(AND)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------rd ddddrrrr");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.SourceRegister, vals['r']);
                yield break;
            }
            if (type == typeof(ADIW))
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
            if (   type == typeof(ANDI)
                || type == typeof(CBR)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("----KKKK ddddKKKK");
                yield return new Operand(OperandType.DestinationRegister, 16 + vals['d']);
                yield return new Operand(OperandType.ConstantData, vals['K']);
                yield break;
            }
            if (type == typeof(ASR))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd----");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield break;
            }
            if (   type == typeof(BCLR)
                || type == typeof(BSET)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- -sss----");
                yield return new Operand(OperandType.StatusRegisterBit, vals['s']);
                yield break;
            }
            if (   type == typeof(BLD)
                || type == typeof(BST)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------d dddd-bbb");
                yield return new Operand(OperandType.DestinationRegister, vals['d']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }
            if (   type == typeof(BRBC)
                || type == typeof(BRBS)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkksss");
                yield return new Operand(OperandType.StatusRegisterBit, vals['s']);
                yield return new Operand(OperandType.ConstantAddress, CalculateTwosComplement(vals['k'], 7));
                yield break;
            }
            if (   type == typeof(BRCC)
                || type == typeof(BRCS)
                || type == typeof(BREQ)
                || type == typeof(BRGE)
                || type == typeof(BRHC)
                || type == typeof(BRHS)
                || type == typeof(BRID)
                || type == typeof(BRIE)
                || type == typeof(BRLO)
                || type == typeof(BRLT)
                || type == typeof(BRMI)
                || type == typeof(BRNE)
                || type == typeof(BRPL)
                || type == typeof(BRSH)
                || type == typeof(BRTC)
                || type == typeof(BRTS)
                || type == typeof(BRVC)
                || type == typeof(BRVS)
               )
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("------kk kkkkk---");
                yield return new Operand(OperandType.ConstantAddress, CalculateTwosComplement(vals['k'], 7));
                yield break;
            }
            if (   type == typeof(CALL)
                || type == typeof(JMP)
               )
            {
                // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are 
                // always on even boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                var vals = new[] { bytes[0], bytes[1], bytes[2], bytes[3] }
                    .MapToMask("-------k kkkk---k kkkkkkkk kkkkkkkk");

                var addressVal = vals['k'] << 1;
                yield return new Operand(OperandType.ConstantAddress, addressVal);
                yield break;
            }
            if (type == typeof(CBI))
            {
                var vals = new[] { bytes[0], bytes[1] }.MapToMask("-------- AAAAAbbb");
                yield return new Operand(OperandType.IOLocation, vals['A']);
                yield return new Operand(OperandType.BitRegisterIO, vals['b']);
                yield break;
            }
            if (   type == typeof(BREAK)
                || type == typeof(CLC)
                || type == typeof(CLH)
                || type == typeof(CLI)
                || type == typeof(CLN)
               )
            {
                // No operands
                yield break;
            }

            #endregion

            #region Pseudoinstructions

            if (type == typeof(DATA))
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
