namespace AVRDisassembler.InstructionSet.Operands
{
    public enum RepresentationMode
    {
        UNKNOWN,
        FullBytes,
        Register,
        PointerRegister,
        Hexadecimal,
        RelativeOffset
    }
}
