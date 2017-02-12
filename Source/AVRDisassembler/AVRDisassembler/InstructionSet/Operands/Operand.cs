using System.Linq;

namespace AVRDisassembler.InstructionSet.Operands
{
    public class Operand : IOperand
    {
        public OperandType Type { get; set; }
        public byte[] Bytes { get; set; }
        public int Value { get; set; }
        public bool Increment { get; set; }
        public bool Decrement { get; set; }

        public Operand(OperandType type, bool increment, bool decrement)
        {
            Type = type;
            Increment = increment;
            Decrement = decrement;
        }

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
                    return $"0x{string.Join("", Bytes.Select(x => string.Format("{0:X2}", x)))}"
                            .ToLowerInvariant();
                }
                case OperandType._XRegister:
                case OperandType._YRegister:
                case OperandType._ZRegister:
                {
                    var tp = Type == OperandType._XRegister ? "X"
                            : Type == OperandType._YRegister ? "Y" : "Z";

                    return string.Format("{0}{1}{2}", 
                        Decrement ? "-" : string.Empty, 
                        tp,
                        Increment ? "+" : string.Empty);
                }
                case OperandType.DestinationRegister:
                case OperandType.SourceRegister:
                {
                    return $"r{Value}";
                }
                case OperandType.ConstantData:
                case OperandType.ConstantAddress:
                case OperandType.IOLocation:
                {
                    return string.Format("0x{0:X2}", Value).ToLowerInvariant();
                }
                default:
                {
                    return Value.ToString();
                }
            }
        }
    }
}
