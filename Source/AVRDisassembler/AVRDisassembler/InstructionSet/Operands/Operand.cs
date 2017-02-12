using System.Linq;

namespace AVRDisassembler.InstructionSet.Operands
{
    public class Operand : IOperand
    {
        public OperandType Type { get; set; }
        public RepresentationMode RepresentationMode { get; set; }
        public byte[] Bytes { get; set; }
        public int Value { get; set; }
        public bool Increment { get; set; }
        public bool Decrement { get; set; }

        public Operand(OperandType type)
        {
            Type = type;
            switch (Type)
            {
                case OperandType._RawData:
                    RepresentationMode = RepresentationMode.FullBytes;
                    break;
                case OperandType.DestinationRegister:
                case OperandType.SourceRegister:
                    RepresentationMode = RepresentationMode.Register;
                    break;
                case OperandType._XRegister:
                case OperandType._YRegister:
                case OperandType._ZRegister:
                    RepresentationMode = RepresentationMode.PointerRegister;
                    break;
                case OperandType.ConstantData:
                case OperandType.ConstantAddress:
                case OperandType.IOLocation:
                    RepresentationMode = RepresentationMode.Hexadecimal;
                    break;
            }
        }

        public Operand(OperandType type, bool increment, bool decrement)
            : this(type)
        {
            Increment = increment;
            Decrement = decrement;
        }

        public Operand(OperandType type, int val)
            : this(type)
        {
            Type = type;
            Value = val;
        }

        public Operand(OperandType type, byte[] bytes)
            : this(type)
        {
            Type = type;
            Bytes = bytes;
        }

        public override string ToString()
        {
            switch (RepresentationMode)
            {
                case RepresentationMode.FullBytes:
                    return $"0x{string.Join("", Bytes.Select(x => string.Format("{0:X2}", x)))}"
                            .ToLowerInvariant();
                case RepresentationMode.Register:
                    return $"r{Value}";
                case RepresentationMode.PointerRegister:
                    return string.Format("{0}{1}{2}", 
                        Decrement ? "-" : string.Empty,
                        Type == OperandType._XRegister ? "X" : Type == OperandType._YRegister ? "Y" : "Z",
                        Increment ? "+" : string.Empty);
                case RepresentationMode.Hexadecimal:
                    return string.Format("0x{0:X2}", Value).ToLowerInvariant();
                case RepresentationMode.RelativeOffset:
                    return string.Format(
                        ".{0}{1}", Value >= 0 ? "+" : string.Empty, Value);
                default:
                    return Value.ToString();
            }
        }
    }
}
