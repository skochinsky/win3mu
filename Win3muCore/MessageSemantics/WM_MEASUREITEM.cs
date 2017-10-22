using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class WM_MEASUREITEM : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            unsafe
            {
                // Convert
                var mi16 = machine.ReadStruct<Win16.MEASUREITEMSTRUCT>(msg16.lParam);
                var mi32 = mi16.Convert();

                // Call 
                msg32.wParam = (IntPtr)msg16.wParam;
                msg32.lParam = (IntPtr)(&mi32);
                var retv = callback();

                // Convert back
                mi16 = mi32.Convert();
                machine.WriteStruct(msg16.lParam, mi16);

                return (uint)retv.ToInt32();
            }
        }

        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            var saveSP = machine.sp;
            
            try
            {
                // Convert
                var mi32 = Marshal.PtrToStructure<Win32.MEASUREITEMSTRUCT>(msg32.lParam);
                var mi16 = mi32.Convert();

                var ptr = machine.StackAlloc(mi16);
                msg16.wParam = 0;
                msg16.lParam = ptr;
                var retv = callback();

                // Copy back
                mi32 = mi16.Convert();
                Marshal.StructureToPtr(mi32, msg32.lParam, false);

                return (IntPtr)retv;
            }
            finally
            {
                machine.sp = saveSP;
            }
        }
    }
}
