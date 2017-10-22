using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class HandleMap
    {
        public HandleMap()
        {

        }

        public void DefineMapping(IntPtr handle32, ushort handle16)
        {
            _map16to32.Add(handle16, handle32);
            _map32to16.Add(handle32, handle16);
        }

        ushort AllocateHandle()
        {
            while (_map16to32.ContainsKey(_nextHandle) || _nextHandle<baseHandle)
                _nextHandle++;
            return _nextHandle;
        }

        public void Destroy32(IntPtr handle32)
        {
            if (_map32to16.ContainsKey(handle32))
            {
                _map16to32.Remove(To16(handle32));
                _map32to16.Remove(handle32);
            }
        }

        public void Destroy16(ushort handle16)
        {
            if (_map16to32.ContainsKey(handle16))
            {
                _map32to16.Remove(To32(handle16));
                _map16to32.Remove(handle16);
            }
        }

        public bool IsValid16(ushort handle16)
        {
            return _map16to32.ContainsKey(handle16);
        }

        public ushort To16(IntPtr handle32)
        {
            if (handle32 == IntPtr.Zero)
                return 0;

            ushort handle16;
            if (_map32to16.TryGetValue(handle32, out handle16))
                return handle16;

            handle16 = AllocateHandle();
            _map32to16.Add(handle32, handle16);
            _map16to32.Add(handle16, handle32);

            return handle16;
        }

        public IntPtr To32(ushort handle16)
        {
            if (handle16 == 0)
                return IntPtr.Zero;

            IntPtr handle32;
            if (_map16to32.TryGetValue(handle16, out handle32))
                return handle32;

            Log.WriteLine("Invalid handle - 0x{0:X4}", handle16);
            //throw new InvalidOperationException(string.Format("Unknown 16 bit handle 0x{0:X4}", handle16));
            return (IntPtr)(-1);
        }

        public IEnumerable<IntPtr> GetAll32()
        {
            return _map32to16.Keys;
        }

        const ushort baseHandle = 32;      // Less than this wordzap won't start??
        ushort _nextHandle = baseHandle;
        Dictionary<IntPtr, ushort> _map32to16 = new Dictionary<IntPtr, ushort>();
        Dictionary<ushort, IntPtr> _map16to32 = new Dictionary<ushort, IntPtr>();
    }
}
