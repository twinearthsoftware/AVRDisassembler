namespace ArduinoDisassembler.InstructionSet.OpCodes
{
    public abstract class OpCode : IOpCode
    {
        public virtual string Name => GetType().Name;
    }
}
