using System;
using System.Collections.Generic;
using System.Linq;
using ArduinoDisassembler.InstructionSet.OpCodes;
using ArduinoDisassembler.InstructionSet.Operands;

namespace ArduinoDisassembler
{
    public class AssemblyStatement
    {
        public IEnumerable<byte> OriginalBytes { get; set; }

        public IOpCode OpCode { get; set; }
        public IOperand Operand1 { get; set; }
        public IOperand Operand2 { get; set; }
        public int Offset { get; set; }

        public AssemblyStatement(IOpCode opCode)
        {
            OpCode = opCode;
        }

        public AssemblyStatement(IOpCode opCode, IOperand operand1)
            : this(opCode)
        {
            Operand1 = operand1;
        }

        public AssemblyStatement(IOpCode opCode, IOperand operand1, IOperand operand2)
            : this(opCode, operand1)
        {
            Operand2 = operand2;
        }

        public override string ToString()
        {
            var offset = string.Format("{0:X4}", Offset);
            var originalBytes = BitConverter.ToString(OriginalBytes.ToArray()).PadRight(12);
            var instruction = OpCode.Name;
            var operand1 = Operand1 == null ? string.Empty : $" {Operand1}";
            var operand2 = Operand2 == null ? string.Empty : $", {Operand2}";
            var instructionWithOperands = $"{instruction}{operand1}{operand2}".ToLowerInvariant().PadRight(24);
            var comment = $"; {OpCode.Comment}";

            return $"{offset}:\t{originalBytes}\t{instructionWithOperands}{comment}";
        }
    }
}
