using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore.MessageSemantics
{
    class WM_GETTEXT : Callable
    {
        public override uint Call32from16(Machine machine, bool hook, bool dlgproc, ref Win16.MSG msg16, ref Win32.MSG msg32, Func<IntPtr> callback)
        {
            unsafe
            {
                var buf = new char[msg16.wParam];

                fixed (char* psz = buf)
                {
                    msg32.wParam = (IntPtr)msg16.wParam;
                    msg32.lParam = (IntPtr)psz;
                    var len = callback().ToInt32();
                    if (len >= 0)
                    {
                        var str = new String(psz, 0, len);
                        return machine.WriteString(msg16.lParam, str, msg16.wParam);
                    }
                    else
                    {
                        return (uint)(len);
                    }
                }
            }
        }

        public override IntPtr Call16from32(Machine machine, bool hook, bool dlgproc, ref Win32.MSG msg32, ref Win16.MSG msg16, Func<uint> callback)
        {
            var ptr = machine.SysAlloc(msg32.wParam);
            msg16.wParam = msg32.wParam.ToInt32().Loword();
            msg16.lParam = ptr;
            var retv = callback();
            if (retv >= 0)
            {
                var str = machine.ReadString(ptr);
                var unibytes = Encoding.Unicode.GetBytes(str);
                Marshal.Copy(unibytes, 0, msg32.lParam, Math.Min((int)msg32.wParam, str.Length * 2));
            }
            machine.SysFree(ptr);
            return (IntPtr)retv;
        }
    }
}
