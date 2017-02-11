using System;
using System.Collections.Generic;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.Operands;

namespace AVRDisassembler
{
    public static class OperandExtraction
    {
        public static IEnumerable<IOperand> ExtractOperands(Type type, byte[] bytes)
        {
            if (type == typeof(JMP))
            {
                // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are 
                // always on even boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                var interpreted = new[] { bytes[2], bytes[3] }.MapToMask("kkkkkkkk kkkkkkkk");
                var addressVal = interpreted['k'] << 1;
                yield return new IntegerOperand(addressVal);
            }
        }
    }
}
