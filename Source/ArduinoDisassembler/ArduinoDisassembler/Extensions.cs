using System;
using System.Collections.Generic;
using IntelHexFormatReader.Model;

namespace ArduinoDisassembler
{
    internal static class Extensions
    {
        internal static byte[] ReadWord(this IEnumerator<MemoryCell> enumerator, Endianness endianness)
        {
            enumerator.MoveNext();
            var byte1 = enumerator.Current.Value;
            enumerator.MoveNext();
            var byte2 = enumerator.Current.Value;
            return endianness == Endianness.BigEndian
                ? new[] {byte1, byte2}
                : new[] {byte2, byte1};
        }

        internal static int WordValue(this byte[] word)
        {
            var length = word.Length;
            if (length > 4 || length == 0)
                throw new ArgumentException($"Length of byte array ({length}) not within boundaries!");

            var result = 0;
            for (var i = length - 1; i >= 0; i--)
                result += word[i] << (8 * (length - 1 - i));

            return result;
        }
    }
}
