namespace AVRDisassembler.InstructionSet.Operands
{
    public enum OperandType
    {
        _RawData,               // Pseudoinstruction

        _XRegister,             // X
        _YRegister,             // Y
        _ZRegister,             // Z

        DestinationRegister,    // Rd
        SourceRegister,         // Rr
        ConstantData,           // K
        ConstantAddress,        // k
        BitRegisterIO,          // b
        StatusRegisterBit,      // s
        IOLocation,             // A
    }
}
