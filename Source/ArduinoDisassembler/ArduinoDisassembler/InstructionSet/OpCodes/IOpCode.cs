namespace ArduinoDisassembler.InstructionSet.OpCodes
{
    public interface IOpCode
    {
        string Name { get; }
        string Comment { get; }
    }
}
