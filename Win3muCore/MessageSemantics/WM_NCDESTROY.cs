using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_NCDESTROY : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            msg32.wParam = IntPtr.Zero;
            msg32.lParam = IntPtr.Zero;
            callback();
            return 0;
        }
        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            msg16.wParam = 0;
            msg16.lParam = 0;
            callback();
            if (!hook)
                HWND.LeaveDestroy();
            return IntPtr.Zero;
        }
    }
}
