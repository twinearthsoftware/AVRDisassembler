using System;
using System.Collections.Generic;
using System.Linq;
using ArduinoDisassembler.OpCodes;
using IntelHexFormatReader;
using IntelHexFormatReader.Model;

namespace ArduinoDisassembler
{
    internal class Disassembler
    {
        private const int UnoR3MemSize = 32768;

        private readonly DisassemblerOptions _options;
        private readonly Dictionary<Type, Func<OpCode, IEnumerator<MemoryCell>, AssemblyStatement>> _handlers =
            new Dictionary<Type, Func<OpCode, IEnumerator<MemoryCell>, AssemblyStatement>>
            {
                {
                    typeof(JMP), (opCode, enumerator) =>
                    {
                        // To save a bit, the address is shifted on to the right prior to storing (this works because jumps are always on even 
                        // boundaries). The MCU knows this, so shifts the addrss one to the left when loading it.
                        var opBytes = enumerator.ReadWord(Endianness.LittleEndian);
                        var addressVal = opBytes.WordValue() << 1;
                        var addressOp = new Operand(addressVal, opBytes);
                        return new AssemblyStatement(opCode, addressOp);
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

            IEnumerator<MemoryCell> enumerator = 
                memoryRepresentation.Cells.ToList().GetEnumerator();

            while (enumerator.MoveNext())
            {
                var word = enumerator.ReadWord(Endianness.LittleEndian);

                var opcode = IdentifyOpCode(word);
                if (opcode == null) throw new NotImplementedException();

                var type = opcode.GetType();
                if (!_handlers.ContainsKey(type)) throw new NotImplementedException();

                var handler = _handlers[type];
                var statement = handler(opcode, enumerator);
                yield return statement;
            }
        }

        private static OpCode IdentifyOpCode(byte[] word)
        {
            var wordVal = word.WordValue();

            switch (wordVal)
            {
                case 0x940c:    return new JMP(word);
                default:        return null;
            }
        }
    }
}
