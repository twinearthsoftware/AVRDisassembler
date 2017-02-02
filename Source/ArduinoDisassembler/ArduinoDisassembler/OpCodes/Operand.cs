namespace ArduinoDisassembler.OpCodes
{
    public class Operand : IOperand
    {
        public int Value { get; set; }
        public byte[] OriginalBytes { get; set; }

        public Operand(int value, byte[] bytes)
        {
            Value = value;
            OriginalBytes = bytes;
        }
    }
}
