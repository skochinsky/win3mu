using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_TIMER : Postable
    {
        public override bool ShouldBypass(Machine machine, ref Win32.MSG msg)
        {
            if (msg.wParam.Hiword() != 0)
                return true;
            if (msg.lParam == IntPtr.Zero)
                return false;
            return machine.Messaging.GetTimerProc(msg.lParam) == 0;
        }

        public override void To16(Machine machine, ref Win32.MSG msg32, ref Win16.MSG msg16)
        {
            msg16.wParam = (ushort)msg32.wParam.ToInt32();
            msg16.lParam = machine.Messaging.GetTimerProc(msg32.lParam);
        }

        public override void To32(Machine machine, ref Win16.MSG msg16, ref Win32.MSG msg32)
        {
            msg32.wParam = (IntPtr)msg16.wParam;
            msg32.lParam = machine.Messaging.GetTimerProc(msg16.lParam);
        }
    }
}
