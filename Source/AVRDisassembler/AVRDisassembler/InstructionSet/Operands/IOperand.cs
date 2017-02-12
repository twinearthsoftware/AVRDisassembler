namespace AVRDisassembler.InstructionSet.Operands
{
    public interface IOperand
    {
        OperandType Type { get; set; }
        byte[] Bytes { get; set; }
        int Value { get; set; }
        bool Increment { get; set; }
        bool Decrement { get; set; }
    }
}
