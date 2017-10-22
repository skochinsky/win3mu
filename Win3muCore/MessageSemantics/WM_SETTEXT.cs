using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_SETTEXT : unused_string
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            // UGH!  WM_SETTEXT(hIcon) -> static SS_ICON control  loword of lParam is a HICN
            if (!hook)
            {
                if (msg16.lParam.Hiword() == 0 && msg16.lParam.Loword() != 0)
                {
                    if (WindowClassKind.Get(msg32.hWnd) == WndClassKind.Static
                        && (User._GetWindowLong(msg32.hWnd, Win32.GWL_STYLE) & 0x0F) == 0x03)
                    {
                        HGDIOBJ hIcon = HGDIOBJ.To32(msg16.lParam.Loword());
                        User._SendMessage(msg32.hWnd, Win32.STM_SETICON, hIcon.value, IntPtr.Zero);
                        return 0;
                    }
                }
            }

            return base.Call32from16(machine, hook, dlgproc, ref msg16, ref msg32, callback);
        }
    }
}
