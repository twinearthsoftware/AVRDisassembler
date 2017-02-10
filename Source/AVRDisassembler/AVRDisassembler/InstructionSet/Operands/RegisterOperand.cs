namespace AVRDisassembler.InstructionSet.Operands
{
    public class RegisterOperand : Operand
    {
        public RegisterOperand(int value)
        {
            OperandValue = value;
        }

        public override string ToString()
        {
            return $"r{OperandValue}";
        }
    }
}
