namespace ArduinoDisassembler.OpCodes
{
    public abstract class OpCode : IOpCode
    {
        public byte[] OpCodeBytes { get; set; }

        protected OpCode(byte[] opcodeBytes)
        {
            OpCodeBytes = opcodeBytes;
        }
    }
}
