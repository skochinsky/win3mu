using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore.Debugging
{
    public class WndProcSymbolScope : GenericSymbolScope
    {
        public WndProcSymbolScope(Machine machine)
        {
            _machine = machine;
            RegisterSymbol("wndproc", () => new FarPointer(_machine.cs, _machine.ip));
            RegisterSymbol("hWnd", () => _machine.ReadWord(_machine.ss, (ushort)(_machine.sp + 4)));
            RegisterSymbol("message", () => _machine.ReadWord(_machine.ss, (ushort)(_machine.sp + 6)));
            RegisterSymbol("wParam", () => _machine.ReadWord(_machine.ss, (ushort)(_machine.sp + 8)));
            RegisterSymbol("lParam", () => _machine.ReadDWord(_machine.ss, (ushort)(_machine.sp + 10)));
        }

        Machine _machine;
    }
}
