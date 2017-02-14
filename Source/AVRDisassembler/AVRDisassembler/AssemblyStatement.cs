using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.Operands;

namespace AVRDisassembler
{
    public class AssemblyStatement
    {
        public IEnumerable<byte> OriginalBytes { get; set; }

        public IOpCode OpCode { get; set; }
        public IEnumerable<IOperand> Operands { get; set; }
        public int Offset { get; set; }

        public AssemblyStatement(IOpCode opCode)
        {
            OpCode = opCode;
        }

        public AssemblyStatement(IOpCode opCode, IEnumerable<IOperand> operands)
        {
            OpCode = opCode;
            Operands = operands;
        }

        public override string ToString()
        {
            var offset = string.Format("{0:X4}", Offset);
            var originalBytes = BitConverter.ToString(OriginalBytes.ToArray()).PadRight(12);
            var instruction = OpCode.Name.ToLowerInvariant();
            var operands = string.Join(", ", Operands);
            if (operands != string.Empty) operands = $" {operands}";
            var instructionWithOperands = $"{instruction}{operands}".PadRight(22);
            var comment = $"; {OpCode.Comment}";
            return $"{offset}:\t{originalBytes}\t{instructionWithOperands}{comment}";
        }
    }
}
