using System;
using System.Collections.Generic;
using System.Linq;
using ArduinoDisassembler.InstructionSet.OpCodes;
using ArduinoDisassembler.InstructionSet.Operands;
using IntelHexFormatReader;

namespace ArduinoDisassembler
{
    public class Disassembler
    {
        private const int UnoR3MemSize = 32768;

        private readonly DisassemblerOptions _options;

        private static readonly Func<byte[], OpCode, AssemblyStatement> _instructionWithoutOperand =
            (bytes, opCode) => new AssemblyStatement(opCode); 

        private readonly Dictionary<Type, Func<byte[], OpCode, IEnumerable<IOperand>>> _handlers =
            new Dictionary<Type, Func<byte[], OpCode, IEnumerable<IOperand>>>
            {
                // Instructions
                {
                    typeof(JMP), (bytes, opCode) =>
                    {
                        // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are 
                        // always on even boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                        var operandBytes = new []{ bytes[2], bytes[3]};
                        var addressVal = operandBytes.WordValue() << 1;
                        var addressOperand = new IntegerOperand(addressVal);
                        return new[] {addressOperand};
                    }
                },
                {
                    typeof(CPC), (bytes, opCode) =>
                    {
                        var byte1 = bytes[0];
                        var byte2 = bytes[1];
                        var dReg = ((byte1 & 0x1) << 4) + (byte2 >> 4);
                        var rReg = ((byte1 & 0x2) << 3) + (byte2 & 0x0f);
                        return new[] {new RegisterOperand(dReg), new RegisterOperand(rReg)};
                    }
                },
                {
                    typeof(MULS), (bytes, opCode) =>
                    {
                        var b = bytes[1];
                        var dReg = 16 + (b >> 4);
                        var rReg = 16 + (b & 0x0f);
                        return new [] {new RegisterOperand(dReg), new RegisterOperand(rReg)};
                    }
                },
                {
                    typeof(MULSU), (bytes, opCode) =>
                    {
                        var b = bytes[1];
                        var dReg = 16 + (b >> 4);
                        var rReg = 16 + (b & 0x0f);
                        return new[] {new RegisterOperand(dReg), new RegisterOperand(rReg)};
                    }
                },
                {
                    typeof(NOP), (bytes, opCode) => new List<IOperand>()
                },
                // Pseudoinstructions
                {
                    typeof(DATA), (bytes, opCode) => new[]{new BytesOperand(bytes)}
                }
            };

        internal Disassembler(DisassemblerOptions options)
        {
            _options = options;
        }

        internal IEnumerable<AssemblyStatement> Disassemble()
        {
            var reader = new HexFileReader(_options.File, UnoR3MemSize);
            var memoryRepresentation = reader.Parse();
            var highestOffset = memoryRepresentation.HighestModifiedOffset;

            var enumerator = 
                new MemoryCellEnumerator(memoryRepresentation.Cells);

            while (enumerator.Index < highestOffset)
            {
                enumerator.ClearBuffer();
                var offset = enumerator.Index;
                var bytes = enumerator.ReadWord(Endianness.LittleEndian);

                var opcode = IdentifyOpCode(bytes);

                if (opcode is _32BitOpCode)
                {
                    var extraBytes = enumerator.ReadWord(Endianness.LittleEndian);
                    bytes = bytes.Concat(extraBytes).ToArray();
                }
                var type = opcode.GetType();
                if (!_handlers.ContainsKey(type))
                    throw new NotImplementedException();

                var handler = _handlers[type];
                var statement = new AssemblyStatement(opcode);
                var operands = handler(bytes, opcode).ToList();
                var numberOfOperands = operands.Count();
                if (numberOfOperands > 0) statement.Operand1 = operands[0];
                if (numberOfOperands > 1) statement.Operand2 = operands[1];
                statement.Offset = offset;
                statement.OriginalBytes = enumerator.Buffer;
                yield return statement;
            }
        }

        public static OpCode IdentifyOpCode(byte[] bytes)
        {
            var opcodeHighByte = bytes[0];
            var opcodeLowByte = bytes[1];

            // Instructions
            if (opcodeHighByte >> 2 == 0x07)                                              return new ADC();
            if (opcodeHighByte >> 2 == 0x03)                                              return new ADD();
            if (opcodeHighByte == 0x96)                                                   return new ADIW();
            if (opcodeHighByte >> 2 == 0x08)                                              return new AND();
            if ((opcodeHighByte & 0xf0) == 0x70)                                          return new ANDI();
            if (opcodeHighByte >> 1 == 0x4a && (opcodeLowByte & 0x0f) == 0x05)            return new ASR();
            if (opcodeHighByte == 0x00 && opcodeLowByte == 0x00)                          return new NOP();
            if (opcodeHighByte >> 1 == 0x4a && (opcodeLowByte & 0x0f) >> 1 == 0x6)        return new JMP();                          
            if (opcodeHighByte >> 2 == 1)                                                 return new CPC();
            if (opcodeHighByte == 0x02)                                                   return new MULS();
            if (opcodeHighByte == 0x03)                                                   return new MULSU();

            // Pseudoinstructions
            return new DATA();
        }

        public IEnumerable<IOperand> ParseOperands(OpCode opcode, byte[] bytes)
        {
            var type = opcode.GetType();
            if (!_handlers.ContainsKey(type))
                throw new NotImplementedException();

            var handler = _handlers[type];
            return handler(bytes, opcode);
        }
    }
}
