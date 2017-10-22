using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_XSCROLL : Postable
    {
        public override void To32(Machine machine, ref Win16.MSG msg16, ref Win32.MSG msg32)
        {
            msg32.wParam = BitUtils.MakeIntPtr(msg16.wParam, msg16.lParam.Loword());
            msg32.lParam = HWND.Map.To32(msg16.lParam.Hiword());
        }

        public override void To16(Machine machine, ref Win32.MSG msg32, ref Win16.MSG msg16)
        {
            msg16.wParam = msg32.wParam.Loword();
            msg16.lParam = BitUtils.MakeDWord(msg32.wParam.Hiword(), HWND.Map.To16(msg32.lParam));
        }
    }
}
