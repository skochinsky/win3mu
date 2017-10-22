using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore.Debugging
{
    public class Win3muDebugger : Sharp86.TextGuiDebugger, IWndProcFilter
    {
        public Win3muDebugger(Machine machine)
        {
            BreakPoint.RegisterBreakPointType("wndproc", typeof(WndProcBreakPoint));
            _machine = machine;
            _wndProcSymbolScope = new WndProcSymbolScope(_machine);
            _machine.Messaging.RegisterFilter(this);
        }

        Machine _machine;
        WndProcSymbolScope _wndProcSymbolScope;

        protected override void PrepareBreakPoints()
        {
            if (BreakPoints.OfType<WndProcBreakPoint>().Any())
            {
                foreach (var wpbp in BreakPoints.OfType<WndProcBreakPoint>())
                {
                    wpbp.Prepare(_wndProcSymbolScope);
                }

                _machine.NotifyCallWndProc16 = () =>
                {
                    foreach (var wpbp in BreakPoints.OfType<WndProcBreakPoint>())
                    {
                        wpbp.Break();
                    }
                };
            }
            else
            {
                _machine.NotifyCallWndProc16 = null;
            }

            base.PrepareBreakPoints();
        }

        IntPtr? IWndProcFilter.PreWndProc(uint pfnProc, ref Win32.MSG msg, bool dlgProc)
        {

            if (msg.message == Win32.WM_KEYDOWN && msg.wParam.ToInt32() == Win32.VK_F9)
            {
                Break();
            }

            return null;
        }
    }
}
