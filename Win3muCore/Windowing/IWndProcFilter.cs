using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public interface IWndProcFilter
    {
        IntPtr? PreWndProc(uint pfnProc, ref Win32.MSG msg, bool dlgProc);
    }

}
