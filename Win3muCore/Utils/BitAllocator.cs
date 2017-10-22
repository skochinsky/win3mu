using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    class BitAllocator<T> where T : class
    {
        public BitAllocator(int count)
        {
            _allocated = new T[count];
            _count = count;
        }

        T[] _allocated;
        int _count;
        int _nextAllocation;

        public void Reserve(int index, T value)
        {
            System.Diagnostics.Debug.Assert(_allocated[index]==null);
            _allocated[index] = value;
        }

        public int Allocate(T val)
        {
            while (_allocated[_nextAllocation]!=null)
            {
                _nextAllocation++;
            }

            _allocated[_nextAllocation++] = val;
            return _nextAllocation - 1;
        }

        public void Free(int index)
        {
            System.Diagnostics.Debug.Assert(_allocated[index]!=null);
            _allocated[index] = null;
        }

        public T Get(int index)
        {
            return _allocated[index];
        }
    }
}
