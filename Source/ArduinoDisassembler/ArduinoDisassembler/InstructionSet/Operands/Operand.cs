namespace ArduinoDisassembler.InstructionSet.Operands
{
    public abstract class Operand : IOperand
    {
        public int OperandValue { get; set; }
        public byte[] OperandBytes { get; set; }
    }
}
