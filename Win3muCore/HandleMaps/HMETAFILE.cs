using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HENHMETAFILE
    {
        public HENHMETAFILE(IntPtr handle)
        {
            value = handle;
        }

        public IntPtr value;

        public static implicit operator HENHMETAFILE(IntPtr value) { return new HENHMETAFILE() { value = value }; }
        public static HENHMETAFILE Null = IntPtr.Zero;
        public static HandleMap Map = new HandleMap();
        public static HENHMETAFILE To32(ushort hObj) { return new HENHMETAFILE() { value = Map.To32(hObj) }; }
        public static ushort To16(HENHMETAFILE hObj) { return Map.To16(hObj.value); }
        public static void Destroy(ushort hMenu) { Map.Destroy16(hMenu); }
        public override string ToString()
        {
            return string.Format("HENHMETAFILE(0x{0:X}/0x{1:X})", Map.To16(value), (ulong)value.ToInt64());
        }
    }
}
