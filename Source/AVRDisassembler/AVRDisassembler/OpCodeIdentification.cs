using System.Collections.Generic;
using System.Linq;
using AVRDisassembler.InstructionSet.OpCodes;
using AVRDisassembler.InstructionSet.OpCodes.Arithmetic;
using AVRDisassembler.InstructionSet.OpCodes.Bits;
using AVRDisassembler.InstructionSet.OpCodes.Branch;
using AVRDisassembler.InstructionSet.OpCodes.DataTransfer;
using AVRDisassembler.InstructionSet.OpCodes.MCUControl;

namespace AVRDisassembler
{
    public static class OpCodeIdentification
    {
        // TODO Implement LD Y4 / LD Z4 / ST Y4 / ST Z4
        public static IEnumerable<IOpCode> IdentifyOpCode(byte[] bytes)
        {
            var high = bytes[0];
            var low = bytes[1];
            var nb1 = (byte)(high >> 4);
            var nb2 = (byte)(high & 0x0f);
            var nb3 = (byte)(low >> 4);
            var nb4 = (byte)(low & 0x0f);

            IEnumerable<IOpCode> result = null;
            switch (nb1)
            {
                case 0b0000: result = IdentifyWithFirstNibble0000(nb2, nb3, nb4); break;
                case 0b0001: result = IdentifyWithFirstNibble0001(nb2, nb3, nb4); break;
                case 0b0010: result = IdentifyWithFirstNibble0010(nb2, nb3, nb4); break;
                //case 0b0011: result = IdentifyWithFirstNibble0011(nb2, nb3, nb4); break;
                //case 0b0100: result = IdentifyWithFirstNibble0100(nb2, nb3, nb4); break;
                //case 0b0101: result = IdentifyWithFirstNibble0101(nb2, nb3, nb4); break;
                //case 0b0110: result = IdentifyWithFirstNibble0110(nb2, nb3, nb4); break;
                case 0b0111: result = IdentifyWithFirstNibble0111(nb2, nb3, nb4); break;
                //case 0b1000: result = IdentifyWithFirstNibble1000(nb2, nb3, nb4); break;
                case 0b1001: result = IdentifyWithFirstNibble1001(nb2, nb3, nb4); break;
                //case 0b1010: result = IdentifyWithFirstNibble1010(nb2, nb3, nb4); break;
                //case 0b1011: result = IdentifyWithFirstNibble1011(nb2, nb3, nb4); break;
                //case 0b1100: result = IdentifyWithFirstNibble1100(nb2, nb3, nb4); break;
                //case 0b1101: result = IdentifyWithFirstNibble1101(nb2, nb3, nb4); break;
                //case 0b1110: result = IdentifyWithFirstNibble1110(nb2, nb3, nb4); break;
                case 0b1111: result = IdentifyWithFirstNibble1111(nb2, nb3, nb4); break;
            }
            foreach (var opCode in result)
                yield return opCode;
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble0000(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 2)
            {
                case 0b01: yield return new CPC(); yield break;
                case 0b10: yield return new SBC(); yield break;
                case 0b11: yield return new ADD(); yield return new LSL(); yield break;
            }
            switch (nb2)
            {
                case 0b0000:
                {
                    if (nb3 == 0b0000 && nb4 == 0b0000) yield return new NOP(); yield break;
                }
                case 0b0001: yield return new MOVW(); yield break;
                case 0b0010: yield return new MULS(); yield break;
                case 0b0011:
                {
                    var firstDigitNibble3 = nb3 >> 3;
                    var firstDigitNibble4 = nb4 >> 3;

                    if (firstDigitNibble3 == 0)
                    {
                        yield return firstDigitNibble4 == 0
                            ? new MULSU()
                            : (IOpCode) new FMUL();
                    }
                    else
                    {
                        yield return firstDigitNibble4 == 0
                            ? new FMULS()
                            : (IOpCode) new FMULSU();
                    }
                    break;
                }
            }
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble0001(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 2)
            {
                case 0b00: yield return new CPSE(); yield break;
                case 0b01: yield return new CP(); yield break;
                case 0b10: yield return new SUB(); yield break;
                case 0b11: yield return new ADC(); yield return new ROL();
                break;
            }
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble0010(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 2)
            {
                case 0b00: yield return new AND(); yield return new TST(); yield break;
                case 0b01: yield return new CLR(); yield break;
                case 0b10: yield return new OR(); yield break;
                case 0b11: yield return new MOV(); yield break;
            }
        }

        private static IOpCode IdentifyWithFirstNibble0011(byte nb2, byte nb3, byte nb4)
        {
            return new CPI();
        }

        private static IOpCode IdentifyWithFirstNibble0100(byte nb2, byte nb3, byte nb4)
        {
            return new SBCI();
        }

        private static IOpCode IdentifyWithFirstNibble0101(byte nb2, byte nb3, byte nb4)
        {
            return new SUBI();
        }

        private static IOpCode IdentifyWithFirstNibble0110(byte nb2, byte nb3, byte nb4)
        {
            return new ORI(); // Identical to SBR
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble0111(byte nb2, byte nb3, byte nb4)
        {
            yield return new ANDI(); yield return new CBR();
        }

        private static IOpCode IdentifyWithFirstNibble1000(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 1)
            {
                case 0b000:
                {
                    switch (nb4)
                    {
                        case 0b0000: return new LD(); // Z1
                        case 0b1000: return new LD(); // Y1
                    }
                    break;
                }
                case 0b001:
                {
                    switch (nb4)
                    {
                        case 0b0000: return new ST(); // Z1
                        case 0b1000: return new ST(); // Y1
                    }
                    break;
                }
            }
            return null;
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble1001(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 2)
            {
                case 0b11: yield return new MUL(); yield break;
            }

            switch (nb2 >> 1)
            {
                case 0b000:
                {
                    switch (nb4)
                    {
                        case 0b0000: yield return new LDS32(); yield break;
                        case 0b0001: yield return new LD(); yield break; // Z2
                        case 0b0010: yield return new LD(); yield break; // Z3
                        case 0b0100: yield return new LPM(); yield break; // 2
                        case 0b0101: yield return new LPM(); yield break; // 3
                        case 0b0110: yield return new ELPM(); yield break;
                        case 0b0111: yield return new ELPM(); yield break;
                        case 0b1001: yield return new LD(); yield break; // Y2
                        case 0b1010: yield return new LD(); yield break; // Y3
                        case 0b1100: yield return new LD(); yield break; // X1
                        case 0b1101: yield return new LD(); yield break; // X2
                        case 0b1110: yield return new LD(); yield break; // X3
                        case 0b1111: yield return new POP(); yield break;
                    }
                    break;
                }
                case 0b001:
                {
                    switch (nb4)
                    {
                        case 0b0000: yield return new STS32(); yield break;
                        case 0b0001: yield return new ST(); yield break; // Z2
                        case 0b0010: yield return new ST(); yield break; // Z3
                        case 0b0100: yield return new XCH(); yield break;
                        case 0b0101: yield return new LAS(); yield break;
                        case 0b0110: yield return new LAC(); yield break;
                        case 0b0111: yield return new LAT(); yield break;
                        case 0b1001: yield return new ST(); yield break; // Y2
                        case 0b1010: yield return new ST(); yield break; // Y3
                        case 0b1100: yield return new ST(); yield break; // X1
                        case 0b1101: yield return new ST(); yield break; // X2
                        case 0b1110: yield return new ST(); yield break; // X3
                        case 0b1111: yield return new PUSH(); yield break;
                    }
                    break;
                }
                case 0b010:
                {
                    switch (nb4 >> 1)
                    {
                        case 0b110: yield return new JMP(); yield break;
                        case 0b111: yield return new CALL(); yield break;
                    }
                    switch (nb4)
                    {
                        case 0b0000: yield return new COM(); yield break;
                        case 0b0001: yield return new NEG(); yield break;
                        case 0b0010: yield return new SWAP(); yield break;
                        case 0b0011: yield return new INC(); yield break;
                        case 0b0101: yield return new ASR(); yield break;
                        case 0b0110: yield return new LSR(); yield break;
                        case 0b0111: yield return new ROR(); yield break;
                        case 0b1010: yield return new DEC(); yield break;
                    }
                    break;
                }
            }

            switch (nb2)
            {
                case 0b0100:
                {
                    switch (nb4)
                    {
                        case 0b1000:
                        {
                            if (nb3 >> 3 == 0b1) yield return new BCLR();
                            switch (nb3)
                            {
                                case  0b0000: yield return new SEC(); yield break;
                                case  0b0001: yield return new SEZ(); yield break;
                                case  0b0010: yield return new SEN(); yield break;
                                case  0b0011: yield return new SEV(); yield break;
                                case  0b0100: yield return new SES(); yield break;
                                case  0b0110: yield return new SET(); yield break;
                                case  0b0101: yield return new SEH(); yield break;
                                case  0b0111: yield return new SEI(); yield break;
                                case  0b1000: yield return new CLC(); yield break;
                                case  0b1001: yield return new CLZ(); yield break;
                                case  0b1010: yield return new CLN(); yield break;
                                case  0b1011: yield return new CLV(); yield break;
                                case  0b1100: yield return new CLS(); yield break;
                                case  0b1101: yield return new CLH(); yield break;
                                case  0b1110: yield return new CLT(); yield break;
                                case  0b1111: yield return new CLI(); yield break;
                            }
                            if (nb3 >> 3 == 0b0) yield return new BSET(); yield break;
                        }
                        case 0b1001:
                        {
                            switch (nb3)
                            {
                                case 0b0000: yield return new IJMP(); yield break;
                                case 0b0001: yield return new EIJMP(); yield break;
                            }
                            break;
                        }
                        case 0b1011:
                        {
                            yield return new DES(); yield break;
                        }
                    }
                    break;
                }
                case 0b0101:
                {
                    switch (nb4)
                    {
                        case 0b1000:
                        {
                            switch (nb3)
                            {
                                case 0b0000: yield return new RET(); yield break;
                                case 0b0001: yield return new RETI(); yield break;
                                case 0b1000: yield return new SLEEP(); yield break;
                                case 0b1001: yield return new BREAK(); yield break;
                                case 0b1010: yield return new WDR(); yield break;
                                case 0b1100: yield return new LPM(); yield break; // 1
                                case 0b1101: yield return new ELPM(); yield break;
                                case 0b1110: yield return new SPM(); yield break; // 1
                            }
                            break;
                        }
                        case 0b1001:
                        {
                            switch (nb3)
                            {
                                case 0b0000: yield return new ICALL(); yield break;
                                case 0b0001: yield return new EICALL(); yield break;
                            }
                            break;
                        }
                    }
                    break;
                }
                case 0b0110: yield return new ADIW(); yield break;
                case 0b0111: yield return new SBIW(); yield break;
                case 0b1000: yield return new CBI(); yield break;
                case 0b1001: yield return new SBIC(); yield break;
                case 0b1010: yield return new SBI(); yield break;
                case 0b1011: yield return new SBIS(); yield break;
            }
        }

        private static IOpCode IdentifyWithFirstNibble1010(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 3)
            {
                case 0b0: return new LDS16();
                case 0b1: return new STS16();
            }
            return null;
        }

        private static IOpCode IdentifyWithFirstNibble1011(byte nb2, byte nb3, byte nb4)
        {
            switch (nb2 >> 3)
            {
                case 0: return new IN();
                case 1: return new OUT();
            }
            return null;
        }

        private static IOpCode IdentifyWithFirstNibble1100(byte nb2, byte nb3, byte nb4)
        {
            return new RJMP();
        }

        private static IOpCode IdentifyWithFirstNibble1101(byte nb2, byte nb3, byte nb4)
        {
            return new RCALL();
        }

        private static IOpCode IdentifyWithFirstNibble1110(byte nb2, byte nb3, byte nb4)
        {
            if (nb2 == 0b1111 && nb4 == 0b1111) return new SER(); // Identical to LDI with all bits set
            return new LDI();
        }

        private static IEnumerable<IOpCode> IdentifyWithFirstNibble1111(byte nb2, byte nb3, byte nb4)
        {
            switch (nb4 >> 3)
            {
                case 0b0:
                {
                    switch (nb2 >> 1)
                    {
                        case 0b100: yield return new BLD(); yield break;
                        case 0b101: yield return new BST(); yield break;
                        case 0b110: yield return new SBRC(); yield break;
                        case 0b111: yield return new SBRS(); yield break;
                    }
                    break;
                }
            }

            switch (nb2 >> 2)
            {
                case 0b00:
                {
                    yield return new BRBS();
                    switch (nb4 << 1)
                    {
                        case 0b0000: yield return new BRCS(); yield return new BRLO(); yield break;
                        case 0b0010: yield return new BREQ(); yield break;
                        case 0b0100: yield return new BRMI(); yield break;
                        case 0b0110: yield return new BRVS(); yield break;
                        case 0b1000: yield return new BRLT(); yield break;
                        case 0b1010: yield return new BRHS(); yield break;
                        case 0b1100: yield return new BRTS(); yield break;
                        case 0b1110: yield return new BRIE(); yield break;
                    }
                    break;
                }
                case 0b01:
                {
                    yield return new BRBC();
                    switch (nb4 << 1)
                    {
                        case 0b0000: yield return new BRCC(); yield return new BRSH(); yield break;
                        case 0b0010: yield return new BRNE(); yield break;
                        case 0b0100: yield return new BRPL(); yield break;
                        case 0b0110: yield return new BRVC(); yield break;
                        case 0b1000: yield return new BRGE(); yield break;
                        case 0b1010: yield return new BRHC(); yield break;
                        case 0b1100: yield return new BRTC(); yield break;
                        case 0b1110: yield return new BRID(); yield break;
                    }
                    break;
                }
            }
        }
    }
}
