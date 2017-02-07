namespace ArduinoDisassembler.InstructionSet.OpCodes
{
    public abstract class OpCode : IOpCode
    {
        public abstract string Comment { get; }
        public virtual string Name => GetType().Name;
    }
}
