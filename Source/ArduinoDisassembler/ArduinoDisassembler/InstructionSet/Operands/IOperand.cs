namespace ArduinoDisassembler.InstructionSet.Operands
{
    public interface IOperand
    {
        int OperandValue { get; set; }
        byte[] OperandBytes { get; set; }
    }
}
