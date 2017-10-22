using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HDC
    {
        public IntPtr value;

        public static implicit operator HDC(IntPtr value) { return new HDC() { value = value }; }
        public static HDC Null = IntPtr.Zero;
        public static HandleMap Map = new HandleMap();
        public static HDC To32(ushort hDC) { return new HDC() { value = Map.To32(hDC) }; }
        public static ushort To16(HDC hDC) { return Map.To16(hDC.value); }
        public static void Destroy(ushort hDC) { Map.Destroy16(hDC); }
        public override string ToString()
        {
            return string.Format("HDC(0x{0:X}/0x{1:X})", Map.To16(value), (ulong)value.ToInt64());
        }
    }
}
