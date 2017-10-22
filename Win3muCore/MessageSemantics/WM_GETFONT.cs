using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_GETFONT : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            unsafe
            {
                msg32.wParam = IntPtr.Zero;
                msg32.lParam = IntPtr.Zero;
                return HGDIOBJ.To16(callback());
            }
        }

        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            msg16.wParam = 0;
            msg16.lParam = 0;
            var retv = callback();
            return HGDIOBJ.Map.To32(retv.Loword());
        }
    }
}
