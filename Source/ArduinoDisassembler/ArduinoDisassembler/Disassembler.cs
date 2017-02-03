using System;
using System.Collections.Generic;
using ArduinoDisassembler.InstructionSet.OpCodes;
using ArduinoDisassembler.InstructionSet.Operands;
using IntelHexFormatReader;
using IntelHexFormatReader.Model;

namespace ArduinoDisassembler
{
    internal class Disassembler
    {
        private const int UnoR3MemSize = 32768;

        private readonly DisassemblerOptions _options;

        private static readonly Func<byte[], OpCode, IEnumerator<MemoryCell>, AssemblyStatement> _instructionWithoutOperand =
            (bytes, opCode, enumerator) => new AssemblyStatement(opCode); 

        private readonly Dictionary<Type, Func<byte[], OpCode, IEnumerator<MemoryCell>, AssemblyStatement>> _handlers =
            new Dictionary<Type, Func<byte[], OpCode, IEnumerator<MemoryCell>, AssemblyStatement>>
            {
                // Instructions
                {
                    typeof(JMP), (bytes, opCode, enumerator) =>
                    {
                        // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are 
                        // always on even boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                        var operandBytes = enumerator.ReadWord(Endianness.LittleEndian);
                        var addressVal = operandBytes.WordValue() << 1;
                        var addressOperand = new IntegerOperand(addressVal);
                        return new AssemblyStatement(opCode, addressOperand);
                    }
                },
                {
                    typeof(CPC), (bytes, opCode, enumerator) =>
                    {
                        var byte1 = bytes[0];
                        var byte2 = bytes[1];
                        var dReg = ((byte1 & 0x1) << 4) + (byte2 >> 4);
                        var rReg = ((byte1 & 0x2) << 3) + (byte2 & 0x0f);
                        return new AssemblyStatement(opCode,
                            new RegisterOperand(dReg), new RegisterOperand(rReg));
                    }
                },
                {
                    typeof(NOP), _instructionWithoutOperand
                },
                // Pseudoinstructions
                {
                    typeof(WORD), (bytes, opCode, enumerator) =>
                    {
                        var operand = new BytesOperand(bytes);
                        return new AssemblyStatement(opCode, operand);
                    }
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
                var word = enumerator.ReadWord(Endianness.LittleEndian);

                var opcode = IdentifyOpCode(word);

                var type = opcode.GetType();
                if (!_handlers.ContainsKey(type)) throw new NotImplementedException();

                var handler = _handlers[type];
                var statement = handler(word, opcode, enumerator);
                statement.Offset = offset;
                statement.OriginalBytes = enumerator.Buffer;
                yield return statement;
            }
        }

        private static OpCode IdentifyOpCode(byte[] word)
        {
            var wordVal = word.WordValue();

            // Instructions
            if (wordVal == 0x0000)                          return new NOP();
            if (wordVal == 0x940c)                          return new JMP();
            if (wordVal >> 10 == 1)                         return new CPC();

            // Pseudoinstructions
            return new WORD();
        }
    }
}
