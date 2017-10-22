using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    // WM_CTLCOLOR -> WM_CTLCOLORxxx mapping
    class WM_CTLCOLOR : Callable
    {
        const ushort WM_CTLCOLOR16 = 0x0019;
        const uint WM_CTLCOLORMSGBOX = 0x0132;
        const uint WM_CTLCOLOREDIT = 0x0133;
        const uint WM_CTLCOLORLISTBOX = 0x0134;
        const uint WM_CTLCOLORBTN = 0x0135;
        const uint WM_CTLCOLORDLG = 0x0136;
        const uint WM_CTLCOLORSCROLLBAR = 0x0137;
        const uint WM_CTLCOLORSTATIC = 0x0138;


        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            System.Diagnostics.Debug.Assert(msg16.message == WM_CTLCOLOR16);
            System.Diagnostics.Debug.Assert(msg16.lParam.Hiword() >= 0 && msg16.lParam.Hiword() <= 6);

            msg32.message = WM_CTLCOLORMSGBOX + msg16.lParam.Hiword();
            msg32.wParam = HDC.To32(msg16.wParam).value;
            msg32.lParam = HWND.To32(msg16.lParam.Loword()).value;

            // Call 32-bit proc
            IntPtr hBrush = callback();

            // Map it
            return HGDIOBJ.To16(hBrush);
        }

        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            System.Diagnostics.Debug.Assert(msg32.message != WM_CTLCOLOR16);

            msg16.message = WM_CTLCOLOR16;
            msg16.wParam = HDC.To16(msg32.wParam);
            msg16.lParam = BitUtils.MakeDWord(HWND.To16(msg32.lParam), (ushort)(msg32.message - WM_CTLCOLORMSGBOX));

            uint retv = callback();

            if (dlgproc)
            {
                if (retv == 0)
                    return IntPtr.Zero;

                retv = User._GetWindowLong(msg32.hWnd, Win32.DWL_MSGRESULT);
            }

            // Map the returned brush
            return HGDIOBJ.To32(retv.Loword()).value;
        }
    }
}
