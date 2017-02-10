namespace AVRDisassembler.InstructionSet.OpCodes
{
    public interface IOpCode
    {
        OpCodeSize Size { get; }
        string Name { get; }
        string Comment { get; }
    }
}
