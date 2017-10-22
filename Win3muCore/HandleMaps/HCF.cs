using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HCF
    {
        public IntPtr value;

        public static implicit operator HCF(IntPtr value) { return new HCF() { value = value }; }
        public static HCF Null = IntPtr.Zero;
        public static HandleMap Map = new HandleMap();
        public static HCF To32(ushort HCF) { return new HCF() { value = Map.To32(HCF) }; }
        public static ushort To16(HCF HCF) { return Map.To16(HCF.value); }
        public static void Destroy(ushort HCF) { Map.Destroy16(HCF); }
        public override string ToString()
        {
            return string.Format("HCF(0x{0:X}/0x{1:X})", Map.To16(value), (ulong)value.ToInt64());
        }
    }
}
