using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HGDIOBJ
    {
        public HGDIOBJ(IntPtr handle)
        {
            value = handle;
        }

        public IntPtr value;

        public static implicit operator HGDIOBJ(IntPtr value) { return new HGDIOBJ() { value = value }; }
        public static HGDIOBJ Null = IntPtr.Zero;
        public static HandleMap Map = new HandleMap();
        public static HGDIOBJ To32(ushort hObj) { return new HGDIOBJ() { value = Map.To32(hObj) }; }
        public static ushort To16(HGDIOBJ hObj) { return Map.To16(hObj.value); }
        public static void Destroy(ushort hMenu) { Map.Destroy16(hMenu); }
        public override string ToString()
        {
            return string.Format("HGDIOBJ(0x{0:X}/0x{1:X})", Map.To16(value), (ulong)value.ToInt64());
        }
    }
}
