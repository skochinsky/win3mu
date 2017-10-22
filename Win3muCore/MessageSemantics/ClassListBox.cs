using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class ClassListBox
    {
        const uint LBS_OWNERDRAWFIXED = 0x0010;
        const uint LBS_OWNERDRAWVARIABLE = 0x0020;
        const uint LBS_HASSTRINGS = 0x0040;

        static bool HasStrings(IntPtr hWnd)
        {
            var style = User._GetWindowLong(hWnd, Win32.GWL_STYLE);
            if ((style & (LBS_OWNERDRAWFIXED | LBS_OWNERDRAWVARIABLE)) == 0)
                return true;
            return (style & LBS_HASSTRINGS)!= 0;
        }

        public class LB_ADDSTRING : copy_string
        {
            public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
            {
                if (HasStrings(msg32.hWnd))
                    return base.Call16from32(machine, hook, dlgproc, ref msg32, ref msg16, callback);

                msg16.wParam = msg32.wParam.Loword();
                msg16.lParam = msg32.lParam.Loword();
                return BitUtils.DWordToIntPtr(callback());
            }


            public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
            {
                if (HasStrings(msg32.hWnd))
                    return base.Call32from16(machine, hook, dlgproc, ref msg16, ref msg32, callback);

                msg32.wParam = BitUtils.DWordToIntPtr(msg16.wParam);
                msg32.lParam = BitUtils.DWordToIntPtr(msg16.lParam);
                return callback().DWord();
            }
        }

        /*
        public class LB_DIR : copy_string
        {
            public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
            {
                if (hook)
                    return 0;
                System.Diagnostics.Debug.Assert(!dlgproc);

                // Get the filespec
                string text = null;
                if (msg32.lParam != IntPtr.Zero)
                {
                    text = Marshal.PtrToStringUni(msg32.lParam);
                }

                return 0;
            }
        }
        */
    }
}
