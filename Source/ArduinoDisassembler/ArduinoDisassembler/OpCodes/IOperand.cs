namespace ArduinoDisassembler.OpCodes
{
    public interface IOperand
    {
        int Value { get; set; }
        byte[] OriginalBytes { get; set; }
    }
}
