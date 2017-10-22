using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_DRAWITEM : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            var di16 = machine.ReadStruct<Win16.DRAWITEMSTRUCT>(msg16.lParam);
            unsafe
            {
                Win16.DRAWITEMSTRUCT* ptr = &di16;
                msg32.wParam = (IntPtr)msg16.wParam;
                msg32.lParam = (IntPtr)ptr;
                var retv = callback();
                return (uint)retv.ToInt32();
            }
        }

        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            var di32 = Marshal.PtrToStructure<Win32.DRAWITEMSTRUCT>(msg32.lParam);
            var di16 = di32.Convert();
            var saveSP = machine.sp;
            
            try
            {
                // NB: This needs to be on stack - Wordzap incorrectly uses near pointer from lParam and won't work 
                //     if drawitemstruct is in a different segment (see red bar in Skill -> Handcap, comms options)
                var ptr = machine.StackAlloc(di16);
                msg16.wParam = (ushort)msg32.wParam.ToInt32();
                msg16.lParam = ptr;
                var retv = callback();
                return (IntPtr)retv;
            }
            finally
            {
                machine.sp = saveSP;
            }
        }
    }
}
