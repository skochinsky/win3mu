using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class TempContext : IDisposable
    {
        public TempContext(Machine machine)
        {
            _machine = machine;
        }

        Machine _machine;

        public void Dispose()
        {
            for (int i = 0; i < _globalAllocations.Count; i++)
            {
                Marshal.FreeHGlobal(_globalAllocations[i]);
            }
            _globalAllocations.Clear();
        }

        // Allocate a temporary unmanaged strnig
        public IntPtr AllocUnmanagedString(string str)
        {
            var ptr = Marshal.StringToHGlobalUni(str);
            _globalAllocations.Add(ptr);
            return ptr;
        }

        List<IntPtr> _globalAllocations = new List<IntPtr>();
    }

}
