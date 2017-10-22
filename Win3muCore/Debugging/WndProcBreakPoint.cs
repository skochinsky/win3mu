using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore.Debugging
{
    class WndProcBreakPoint : BreakPoint
    {
        public WndProcBreakPoint()
        {
        }
        public override string EditString
        {
            get
            {
                return "wndproc";
            }
        }

        bool _tripped;

        public void Break()
        {
            _tripped = true;
        }

        public override bool ShouldBreak(DebuggerCore debugger)
        {
            bool t = _tripped;
            _tripped = false;
            return t;
        }

        public override string ToString()
        {
            return base.ToString("wndproc");
        }

        ISymbolScope _symbolScope;
        public override Symbol ResolveSymbol(string name)
        {
            var s = _symbolScope.ResolveSymbol(name);
            if (s != null)
                return s;
            return base.ResolveSymbol(name);
        }

        public void Prepare(ISymbolScope scope)
        {
            _symbolScope = scope;
        }
    }
}
    