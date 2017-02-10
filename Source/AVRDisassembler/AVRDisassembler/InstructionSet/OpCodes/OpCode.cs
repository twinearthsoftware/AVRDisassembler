namespace AVRDisassembler.InstructionSet.OpCodes
{
    public abstract class OpCode : IOpCode
    {
        public abstract string Comment { get; }

        public virtual OpCodeSize Size => OpCodeSize._16;
        public virtual string Name => GetType().Name;
    }
}
