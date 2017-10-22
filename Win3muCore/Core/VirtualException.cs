using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class VirtualException : Exception
    {
        public VirtualException(string msg) : base(msg)
        {
        }

        public VirtualException(string msg, params object[] args) : base(string.Format(msg, args))
        {
        }
    }
}
