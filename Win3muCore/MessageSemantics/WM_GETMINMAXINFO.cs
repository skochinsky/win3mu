using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_GETMINMAXINFO : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            unsafe
            {
                var mmi32 = machine.ReadStruct<Win16.MINMAXINFO>(msg16.lParam).Convert();
                msg32.wParam = IntPtr.Zero;
                msg32.lParam = new IntPtr(&mmi32);
                var ret = callback();
                machine.WriteStruct(msg16.lParam, mmi32.Convert());
                return (uint)ret;
            }
        }
        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            // Get the Win32 struct
            var mmi = Marshal.PtrToStructure<Win32.MINMAXINFO>(msg32.lParam);

            // Call 16 bit function
            var ptr = machine.SysAlloc(mmi.Convert());
            msg16.wParam = 0;
            msg16.lParam = ptr;
            var ret = callback();
            mmi = machine.SysReadAndFree<Win16.MINMAXINFO>(ptr).Convert();

            // Return it to Win32
            Marshal.StructureToPtr(mmi, msg32.lParam, true);
            return IntPtr.Zero;
        }
    }
}
