using System;
using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.DataTransfer;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;
using AVRDisassembler.InstructionSet.Operands;
using IntelHexFormatReader;

namespace AVRDisassembler
{
    public class Disassembler
    {
        private readonly DisassemblerOptions _options;

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

                var opcode = IdentifyOpCode(bytes);

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

        // TODO Implement LD Y4 / LD Z4
        public static OpCode IdentifyOpCode(byte[] bytes)
        {
            var high = bytes[0];
            var low = bytes[1];
            var nibble1 = high >> 4;
            var nibble2 = high & 0x0f;
            var nibble3 = low >> 4;
            var nibble4 = low & 0x0f;

            switch (nibble1)
            {
                case 0b0000:
                {
                    switch (nibble2 >> 2)
                    {
                        case 0b01: return new CPC();
                        case 0b11: return new ADD();
                    }

                    switch (nibble2)
                    {
                        case 0b0011:
                        {
                            switch (nibble3 >> 3)
                            {
                                case 0:
                                {
                                    if (nibble4 >> 3 == 1) return new FMUL();
                                    break;
                                }
                                case 1:
                                {
                                    switch (nibble4 >> 3)
                                    {
                                        case 0: return new FMULS();
                                        case 1: return new FMULSU();
                                    }
                                }
                                break;
                            }
                            break;
                        }
                    }
                    break;
                }
                case 0b0001:
                {
                    switch (nibble2 >> 2)
                    {
                        case 0b00: return new CPSE();
                        case 0b01: return new CP();
                        case 0b11: return new ADC();
                    }
                    break;
                }
                case 0b0010:
                {
                    if (nibble2 >> 2 == 0b00) return new AND();
                    if (nibble2 >> 2 == 0b01) return new CLR();
                    break;
                }
                case 0b0011:
                {
                    return new CPI();
                }
                case 0b0111:
                {
                    return new ANDI();
                }
                case 0b1000:
                {
                    switch (nibble2 >> 1)
                    {
                        case 0b000:
                        {
                            switch (nibble4)
                            {
                                case 0b0000: return new LD(); // Z1
                                case 0b1000: return new LD(); // Y1
                            }
                            break;
                        }
                    }
                    break;
                }
                case 0b1001:
                {
                    switch (nibble2 >> 1)
                    {
                        case 0b000:
                        {
                            switch (nibble4)
                            {
                                case 0b0001: return new LD(); // Z2
                                case 0b0010: return new LD(); // Z3
                                case 0b0110: return new ELPM();
                                case 0b0111: return new ELPM();
                                case 0b1001: return new LD(); // Y2
                                case 0b1010: return new LD(); // Y3
                                case 0b1100: return new LD(); // X1
                                case 0b1101: return new LD(); // X2
                                case 0b1110: return new LD(); // X3
                            }
                            break;
                        }
                        case 0b001:
                        {
                            switch (nibble4)
                            {
                                case 0b101: return new LAS();
                                case 0b110: return new LAC();
                                case 0b111: return new LAT();
                            }
                            break;
                        }
                        case 0b010:
                        {
                            if (nibble4 >> 1 == 0b110) return new JMP();
                            if (nibble4 >> 1 == 0b111) return new CALL();
                            switch (nibble4)
                            {
                                case 0b0000: return new COM();
                                case 0b0011: return new INC();
                                case 0b0101: return new ASR();
                                case 0b1010: return new DEC();
                            }
                            break;
                        }
                    }

                    switch (nibble2)
                    {
                        case 0b0100:
                        {
                            if (nibble4 == 0b1000)
                            {
                                if (nibble3 == 0b1000) return new CLC();
                                if (nibble3 == 0b1001) return new CLZ();
                                if (nibble3 == 0b1010) return new CLN();
                                if (nibble3 == 0b1011) return new CLV();
                                if (nibble3 == 0b1100) return new CLS();
                                if (nibble3 == 0b1101) return new CLH();
                                if (nibble3 == 0b1110) return new CLT();
                                if (nibble3 == 0b1111) return new CLI();
                                if (low >> 7 == 0x00) return new BSET();
                                if (low >> 7 == 0x01) return new BCLR();
                            }
                            if (nibble4 == 0b1011) return new DES();
                            if (nibble4 == 0b1001)
                            {
                                if (nibble3 == 0b0000) return new IJMP();
                                if (nibble3 == 0b0001) return new EIJMP();
                            }
                            break;
                        }
                        case 0b0101:
                        {
                            if (nibble3 == 0b0000 && nibble4 == 0b1001) return new ICALL();
                            if (nibble3 == 0b1101 && nibble4 == 0b1000) return new ELPM();
                            if (nibble3 == 0b1001 && nibble4 == 0b1000) return new BREAK();
                            if (nibble3 == 0b0001 && nibble4 == 0b1001) return new EICALL();
                            break;
                        }
                        case 0b0110:
                        {
                            return new ADIW();
                        }
                        case 0b1000:
                        {
                            return new CBI();
                        }
                    }
                    break;
                }
                case 0b1010:
                {
                    if (nibble2 >> 3 == 0x0) return new LDS16();
                    break;
                }
                case 0b1011:
                {
                    if (nibble2 >> 3 == 0x0) return new IN();
                    break;
                }
                case 0b1110:
                {
                    return new LDI();
                }
                case 0b1111:
                {
                    if (nibble2 >> 1 == 0b100 && nibble4 >> 3 == 0b0) return new BLD();
                    if (nibble2 >> 1 == 0b101 && nibble4 >> 3 == 0b0) return new BST();
                    if (nibble2 >> 2 == 0b01)
                    {
                        if (nibble4 << 1 == 0b1110) return new BRID();
                        if (nibble4 << 1 == 0b1100) return new BRTC();
                        if (nibble4 << 1 == 0b1010) return new BRHC();
                        if (nibble4 << 1 == 0b1000) return new BRGE();
                        if (nibble4 << 1 == 0b0100) return new BRPL();
                        if (nibble4 << 1 == 0b0110) return new BRVC();
                        if (nibble4 << 1 == 0b0010) return new BRNE();
                        if (nibble4 << 1 == 0b0000) return new BRSH();
                    }
                    if (nibble2 >> 2 == 0b00)
                    {
                        if (nibble4 << 1 == 0b1110) return new BRIE();
                        if (nibble4 << 1 == 0b1100) return new BRTS();
                        if (nibble4 << 1 == 0b1010) return new BRHS();
                        if (nibble4 << 1 == 0b1000) return new BRLT();
                        if (nibble4 << 1 == 0b0100) return new BRMI();
                        if (nibble4 << 1 == 0b0110) return new BRVS();
                        if (nibble4 << 1 == 0b0010) return new BREQ();
                        if (nibble4 << 1 == 0b0000) return new BRLO();
                    }
                    break;
                }
            }

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
