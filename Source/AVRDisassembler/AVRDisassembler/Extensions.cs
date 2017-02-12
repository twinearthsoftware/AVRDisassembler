using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntelHexFormatReader.Model;

namespace AVRDisassembler
{
    public static class Extensions
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

        public static IDictionary<char,int> MapToMask(this byte[] bytes, string mask)
        {
            if (mask == null) throw new IOException("Mask can not be null!");
            var length = bytes.Length;
            var maskParts = mask.Split(' ');

            if (maskParts.Length != length || maskParts.Any(maskPart => maskPart.Length != 8))
                throw new IOException(
                    $"Mask definition '{mask}' not consistent with byte "
                    + $"definition '${BitConverter.ToString(bytes)}'!");

            var dictionaryKeys = 
                maskParts.SelectMany(x => x).Distinct().Where(x => x != '-').ToArray();

            var result = dictionaryKeys.ToDictionary(key => key, key => 0);

            foreach (var k in dictionaryKeys)
            {
                var resultForKey = new List<bool>();
                var toggledInMask = 
                    new BitArray(maskParts.SelectMany(x => x.Select(y => y == k)).ToArray());

                var allBitsForVal = new BitArray(bytes.Reverse().ToArray());

                var numberOfBits = toggledInMask.Length;
                for (var i = 0; i < numberOfBits; i++)
                {
                    if (toggledInMask[numberOfBits - i - 1])
                        resultForKey.Add(allBitsForVal[i]);
                }
                result[k] = GetIntFromBitArray(new BitArray(resultForKey.ToArray()));
            }
            return result;
        }

        private static int GetIntFromBitArray(BitArray bitArray)
        {
            var value = 0;
            for (var i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                    value += 1 << i;
            }
            return value;
        }
    }
}
