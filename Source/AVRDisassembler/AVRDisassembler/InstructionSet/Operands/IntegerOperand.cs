namespace AVRDisassembler.InstructionSet.Operands
{
    public class IntegerOperand : Operand
    {
        public IntegerOperand(int value)
        {
            OperandValue = value;
        }

        public override string ToString()
        {
            return string.Format("0x{0:X2}", OperandValue);
        }
    }
}
