using System.Linq;

namespace AVRDisassembler.InstructionSet.Operands
{
    public class BytesOperand : Operand
    {
        public BytesOperand(byte[] bytes)
        {
            OperandBytes = bytes;
        }

        public override string ToString()
        {
            return $"0x{string.Join("", OperandBytes.Select(x => string.Format("{0:X2}", x)))}";
        }
    }
}
