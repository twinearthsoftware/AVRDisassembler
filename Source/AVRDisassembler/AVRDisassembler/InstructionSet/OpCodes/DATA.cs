namespace AVRDisassembler.InstructionSet.OpCodes
{
    public class DATA : PseudoOpCode
    {
        public override string Name => ".data";
        public override string Comment => "Binary data";
    }
}
