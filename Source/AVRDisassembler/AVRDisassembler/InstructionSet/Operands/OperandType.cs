namespace AVRDisassembler.InstructionSet.Operands
{
    public enum OperandType
    {
        _RawData,               // Pseudoinstruction
        DestinationRegister,    // Rd
        SourceRegister,         // Rr
        ConstantData,           // K
        ConstantAddress,        // k
        BitRegisterIO,          // b
        StatusRegisterBit,      // s
        IOLocation              // A
    }
}
