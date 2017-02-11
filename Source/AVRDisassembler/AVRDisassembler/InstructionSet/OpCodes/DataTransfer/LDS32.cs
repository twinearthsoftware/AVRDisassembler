namespace AVRDisassembler.InstructionSet.OpCodes.DataTransfer
{
    public class LDS32 : OpCode
    {
        public override OpCodeSize Size => OpCodeSize._32;
        public override string Name => "LDS";
        public override string Comment => "Load Direct from Data Space (32-bit)";
    }
}
