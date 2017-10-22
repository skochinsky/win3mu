using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore
{
    [Module("KEYBOARD", @"C:\WINDOWS\SYSTEM\KEYBOARD.DRV")]
    public class Keyboard : Module32
    {
        // 0001 - INQUIRE - 0001
        // 0002 - ENABLE - 0002
        // 0003 - DISABLE - 0003
        // 0004 - TOASCII - 0004

        [DllImport("user32.dll")]
        extern static bool CharToOemBuff(string src, [Out] StringBuilder dest, uint cchDest);

        [EntryPoint(0x0005)]
        public short AnsiToOem(string src, uint ptrDest)
        {
            var sb = new StringBuilder(src.Length * 2);
            CharToOemBuff(src, sb, (uint)sb.Capacity);
            var ret = sb.ToString();
            _machine.WriteString(ptrDest.Hiword(), ptrDest.Loword(), sb.ToString(), (ushort)(ret.Length + 1));
            return -1;   
        }

        [DllImport("user32.dll")]
        extern static bool OemToCharBuff(string src, [Out] StringBuilder dest, uint cchDest);

        [EntryPoint(0x0006)]
        public short OemToAnsi(string src, uint ptrDest)
        {
            var sb = new StringBuilder(src.Length * 2);
            OemToCharBuff(src, sb, (uint)sb.Capacity);
            var ret = sb.ToString();
            _machine.WriteString(ptrDest.Hiword(), ptrDest.Loword(), sb.ToString(), (ushort)(ret.Length + 1));
            return -1;
        }

        // 0007 - SETSPEED - 0007
        // 0008 - WEP - 0008
        // 0064 - SCREENSWITCHENABLE - 0064
        // 007E - GETTABLESEG - 007E
        // 007F - NEWTABLE - 007F
        // 0080 - OEMKEYSCAN - 0080
        // 0081 - VKKEYSCAN - 0081
        // 0082 - GETKEYBOARDTYPE - 0082
        // 0083 - MAPVIRTUALKEY - 0083
        // 0084 - GETKBCODEPAGE - 0084
        // 0085 - GETKEYNAMETEXT - 0085
        // 0086 - ANSITOOEMBUFF - 0086
        // 0087 - OEMTOANSIBUFF - 0087
        // 0088 - ENABLEKBSYSREQ - 0088
        // 0089 - GETBIOSKEYPROC - 0089

    }
}
