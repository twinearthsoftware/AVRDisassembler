namespace AVRDisassembler.InstructionSet.OpCodes.Branch
{
    public class JMP : OpCode
    {
        public override OpCodeSize Size => OpCodeSize._32;
        public override string Comment => "Jump";
    }
}
