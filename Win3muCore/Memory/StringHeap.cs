using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class StringHeap
    {
        public StringHeap(Machine machine)
        {
            _machine = machine;
        }

       public uint GetString(string str)
        {
            if (str == null)
                return 0;

            // Already allocated?
            uint ptr;
            if (_strings.TryGetValue(str, out ptr))
            {
                return ptr;
            }

            // First time?
            if (_mem==0)
            {
                _mem = _machine.GlobalHeap.Alloc("System String Heap", 0, 0x10000);
            }

            // Get the buffer and put the string in it
            var buf = _machine.GlobalHeap.GetBuffer(_mem, true);
            int len = buf.WriteString(_ofs, str);

            // Work out pointer for this string
            ptr = BitUtils.MakeDWord(_ofs, _mem);
            _strings.Add(str, ptr);

            // Update high water
            _ofs += (ushort)(len + 1);

            return ptr;
        }

        Machine _machine;
        Dictionary<string, uint> _strings = new Dictionary<string, uint>();
        ushort _mem;
        ushort _ofs;
    }
}
