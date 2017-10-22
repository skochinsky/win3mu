using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_NEXTMENU : Callable
    {
        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            // Convert info
            var info32 = Marshal.PtrToStructure<Win32.MDINEXTMENU>(msg32.lParam);
            var info16 = info32.Convert();

            // Setup message
            msg16.wParam = msg32.lParam.Loword();
            msg16.lParam = machine.SysAlloc(info16);

            try
            {
                // Do it
                return BitUtils.DWordToIntPtr(callback());
            }
            finally
            {
                // Clean up
                machine.SysFree(msg16.lParam);
            }
        }

        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            // Convert info
            var info16 = machine.ReadStruct<Win16.MDINEXTMENU>(msg16.lParam);
            var info32 = info16.Convert();

            unsafe
            {
                // Setup message
                msg32.wParam = (IntPtr)msg16.wParam;
                msg32.lParam = (IntPtr)(&info32);

                // Do it
                return callback().DWord();
            }
        }

    }
}
