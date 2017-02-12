using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using IntelHexFormatReader;

namespace AVRDisassembler
{
    public class Disassembler
    {
        private readonly DisassemblerOptions _options;

        internal Disassembler(DisassemblerOptions options)
        {
            _options = options;
        }

        internal IEnumerable<AssemblyStatement> Disassemble()
        {
            var reader = new HexFileReader(_options.File, 4 * 1024 * 1024);
            var memoryRepresentation = reader.Parse();
            var highestOffset = memoryRepresentation.HighestModifiedOffset;

            var enumerator = 
                new MemoryCellEnumerator(memoryRepresentation.Cells);

            while (enumerator.Index < highestOffset)
            {
                enumerator.ClearBuffer();
                var offset = enumerator.Index;
                var bytes = enumerator.ReadWord(Endianness.LittleEndian);

                var opcodes = OpCodeIdentification.IdentifyOpCode(bytes).ToList();
                // TODO: make a preference configurable if synonyms exist.
                // For now, just pick the first item.
                var opcode = opcodes.Any() ? opcodes.First() : new DATA();

                if (opcode.Size == OpCodeSize._32)
                {
                    var extraBytes = enumerator.ReadWord(Endianness.LittleEndian);
                    bytes = bytes.Concat(extraBytes).ToArray();
                }
                var type = opcode.GetType();

                var statement = new AssemblyStatement(opcode);
                var operands = OperandExtraction.ExtractOperands(type, bytes).ToList();
                    
                var numberOfOperands = operands.Count();
                if (numberOfOperands > 0) statement.Operand1 = operands[0];
                if (numberOfOperands > 1) statement.Operand2 = operands[1];
                statement.Offset = offset;
                statement.OriginalBytes = enumerator.Buffer;
                yield return statement;
            }
        }
    }
}
