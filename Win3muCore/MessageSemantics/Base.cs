using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    abstract class Base
    {
        public Base()
        {
        }

        public abstract bool ShouldBypass(Machine machine, ref Win32.MSG msg);
    }
}
