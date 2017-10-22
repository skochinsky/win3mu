using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class HeapPointer : IDisposable
    {
        public HeapPointer(GlobalHeap heap, uint ptr, bool forWrite)
        {
            if (ptr != 0)
            {
                // Get the buffer
                var buf = heap.GetBuffer(ptr, forWrite, out _ofs);
                _pin = GCHandle.Alloc(buf, GCHandleType.Pinned);
            }
        }

        GCHandle _pin;
        int _ofs;

        public void Dispose()
        {
            if (_pin.IsAllocated)
                _pin.Free();
        }

        public static implicit operator IntPtr(HeapPointer ptr)
        {
            if (ptr._pin.IsAllocated)
                return (IntPtr)(ptr._pin.AddrOfPinnedObject().ToInt64() + ptr._ofs);
            else
                return IntPtr.Zero;
        }
    }
}
