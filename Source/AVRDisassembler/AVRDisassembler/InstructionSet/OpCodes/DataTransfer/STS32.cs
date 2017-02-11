namespace AVRDisassembler.InstructionSet.OpCodes.DataTransfer
{
    public class STS32 : OpCode
    {
        public override OpCodeSize Size => OpCodeSize._32;
        public override string Name => "STS";
        public override string Comment => "Store Direct to Data Space";
    }
}
