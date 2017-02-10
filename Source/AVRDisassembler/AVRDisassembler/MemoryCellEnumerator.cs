using System.Collections;
using System.Collections.Generic;
using IntelHexFormatReader.Model;

namespace AVRDisassembler
{
    internal class MemoryCellEnumerator : IEnumerator<MemoryCell>
    {
        private readonly IEnumerator<MemoryCell> _enumerator;
        private IList<byte> _buffer;
        private int _index;
        public int Index => _index;
        public MemoryCell Current => _enumerator.Current;
        public IList<byte> Buffer => _buffer;
        object IEnumerator.Current => _enumerator.Current;

        public MemoryCellEnumerator(IEnumerable<MemoryCell> cells)
        {
            _enumerator = cells.GetEnumerator();
            _index = 0;
            ClearBuffer();
        }

        public void ClearBuffer()
        {
            _buffer = new List<byte>();
        }

        public bool MoveNext()
        {
            var result = _enumerator.MoveNext();
            if (result)
            {
                _buffer.Add(Current.Value);
                _index++;
            }
            return result;
        }

        public void Reset()
        {
            _enumerator.Reset();
            _index = 0;
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }
    }
}
