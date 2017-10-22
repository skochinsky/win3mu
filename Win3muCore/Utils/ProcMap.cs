using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class ProcMap<TDelegate>
    {
        public IntPtr Connect(TDelegate wndProc32, uint wndProc16)
        {
            IntPtr ptr = Marshal.GetFunctionPointerForDelegate(wndProc32);
            _proc16Map.Add(wndProc16, ptr);
            _proc32Map.Add(ptr, wndProc16);
            _keepAliveMap.Add(wndProc16, wndProc32);
            return ptr;
        }

        public void Connect(IntPtr wndProc32, uint wndProc16)
        {
            _proc16Map.Add(wndProc16, wndProc32);
            _proc32Map.Add(wndProc32, wndProc16);
        }

        public IntPtr To32(uint proc16)
        {
            IntPtr proc32;
            if (_proc16Map.TryGetValue(proc16, out proc32))
                return proc32;
            return IntPtr.Zero;
        }

        public uint To16(IntPtr proc32)
        {
            uint proc16;
            if (_proc32Map.TryGetValue(proc32, out proc16))
                return proc16;
            return 0;
        }

        Dictionary<uint, TDelegate> _keepAliveMap = new Dictionary<uint, TDelegate>();
        Dictionary<uint, IntPtr> _proc16Map = new Dictionary<uint, IntPtr>();
        Dictionary<IntPtr, uint> _proc32Map = new Dictionary<IntPtr, uint>();
    }
}
