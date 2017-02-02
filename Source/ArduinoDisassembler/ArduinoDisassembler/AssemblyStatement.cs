using ArduinoDisassembler.OpCodes;

namespace ArduinoDisassembler
{
    internal class AssemblyStatement
    {
        public IOpCode OpCode { get; set; }
        public IOperand Operand1 { get; set; }
        public IOperand Operand2 { get; set; }

        public AssemblyStatement(IOpCode opCode, IOperand operand1)
        {
            OpCode = opCode;
            Operand1 = operand1;
        }
    }
}
