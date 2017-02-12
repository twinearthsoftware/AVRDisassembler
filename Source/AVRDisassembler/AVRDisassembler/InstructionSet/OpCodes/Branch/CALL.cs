namespace AVRDisassembler.InstructionSet.OpCodes.Branch
{
    public class CALL : OpCode
    {
        public override OpCodeSize Size => OpCodeSize._32;
        public override string Comment => "Long Call to a Subroutine";
    }
}
