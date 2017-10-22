using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_PARENTNOTIFY : Postable
    {
        public override void To32(Machine machine, ref Win16.MSG msg16, ref Win32.MSG msg32)
        {
            if (msg16.wParam == Win16.WM_CREATE || msg16.wParam == Win16.WM_DESTROY)
            {
                // MessageSemantics.command
                msg32.wParam = (IntPtr)BitUtils.MakeDWord(msg16.wParam, msg16.lParam.Hiword());
                msg32.lParam = HWND.Map.To32(msg16.lParam.Loword());
            }
            else
            {
                // MessageSemantics.copy
                msg32.wParam = (IntPtr)msg16.wParam;
                msg32.lParam = (IntPtr)msg16.lParam;
            }
        }

        public override void To16(Machine machine, ref Win32.MSG msg32, ref Win16.MSG msg16)
        {
            if (msg32.wParam.Loword() == Win16.WM_CREATE || msg32.wParam.Loword() == Win16.WM_DESTROY)
            {
                msg16.wParam = msg32.wParam.ToInt32().Loword();
                msg16.lParam = BitUtils.MakeDWord(HWND.Map.To16(msg32.lParam), msg32.wParam.ToInt32().Hiword());
            }
            else
            {
                msg16.wParam = (ushort)msg32.wParam.ToInt32();
                msg16.lParam = (uint)msg32.lParam.ToInt32();
            }
        }
    }
}
