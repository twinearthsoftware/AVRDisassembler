using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using AVRDisassembler.InstructionSet.Operands;
using IntelHexFormatReader;

namespace AVRDisassembler
{
    public class Disassembler
    {
        private readonly DisassemblerOptions _options;

        private readonly Dictionary<Type, Func<byte[], IOpCode, IEnumerable<IOperand>>> _handlers =
            new Dictionary<Type, Func<byte[], IOpCode, IEnumerable<IOperand>>>
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

                var opcodes = OpCodeIdentification.IdentifyOpCode(bytes);
                var opcode = opcodes.First();

                if (opcode.Size == OpCodeSize._32)
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
