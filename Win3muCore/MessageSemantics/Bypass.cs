using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.MessageSemantics
{
    class bypass : Base
    {
        public bypass()
        {
        }

        public override bool ShouldBypass(Machine machine, ref Win32.MSG msg)
        {
            return true;
        }

        public static bypass Instance = new bypass();
    }
}
