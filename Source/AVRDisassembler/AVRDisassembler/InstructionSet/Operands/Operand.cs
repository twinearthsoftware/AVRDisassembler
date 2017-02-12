using System.Linq;
using System.Runtime.InteropServices;

namespace AVRDisassembler.InstructionSet.Operands
{
    public class Operand : IOperand
    {
        public OperandType Type { get; set; }
        public byte[] Bytes { get; set; }
        public int Value { get; set; }

        public Operand(OperandType type, int val)
        {
            Type = type;
            Value = val;
        }

        public Operand(OperandType type, byte[] bytes)
        {
            Type = type;
            Bytes = bytes;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case OperandType._RawData:
                {
                    return $"0x{string.Join("", Bytes.Select(x => string.Format("{0:X2}", x)))}";
                }
                case OperandType.DestinationRegister:
                case OperandType.SourceRegister:
                {
                    return $"r{Value}";
                }
                case OperandType.ConstantData:
                case OperandType.ConstantAddress:
                {
                    return string.Format("0x{0:X2}", Value);
                }
                default:
                {
                    return Value.ToString();
                }
            }
        }
    }
}
