using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HACCEL
    {
        public IntPtr value;

        public static HACCEL Null = new HACCEL() { value = IntPtr.Zero };
        public static HandleMap Map = new HandleMap();
        public static HACCEL To32(ushort h) { return new HACCEL() { value = Map.To32(h) }; }
        public static ushort To16(HACCEL h) { return Map.To16(h.value); }
        public static void Destroy(ushort h) { Map.Destroy16(h); }
    }
}
