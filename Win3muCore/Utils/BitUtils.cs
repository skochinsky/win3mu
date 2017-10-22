using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public static class BitUtils
    {
        public static ushort Hiword(this uint dword)
        {
            return (ushort)(dword >> 16);
        }

        public static ushort Loword(this uint dword)
        {
            return (ushort)(dword & 0xFFFF);
        }

        public static ushort Hiword(this int dword)
        {
            return (ushort)(((uint)dword) >> 16);
        }

        public static ushort Loword(this int dword)
        {
            return (ushort)(dword & 0xFFFF);
        }

        public static uint MakeDWord(ushort lo, ushort hi)
        {
            return (uint)(hi << 16 | lo);
        }

        public static ushort Loword(this IntPtr ip)
        {
            return unchecked((uint)ip.ToInt64()).Loword();
        }

        public static ushort Hiword(this IntPtr ip)
        {
            return unchecked((uint)ip.ToInt64()).Hiword();
        }

        public static uint DWord(this IntPtr ip)
        {
            return unchecked((uint)ip.ToInt64());
        }

        public static IntPtr DWordToIntPtr(uint val)
        {
            return (IntPtr)unchecked((int)val);
        }

        public static IntPtr MakeIntPtr(ushort lo, ushort hi)
        {
            return (IntPtr)unchecked((int)MakeDWord(lo, hi));
        }

        public static uint ToUInt32(this IntPtr This)
        {
            return unchecked((uint)This.ToInt64());
        }
    }
}
