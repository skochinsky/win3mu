using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Win3muRuntime;

namespace Win3mu
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                MessageBox(IntPtr.Zero, "Usage: win3mu <programName> [/debug|/release] [/break] [/config:name]", "Win3mu", 0x10);
            }

            return API.Run(args[0], args.Skip(1).ToArray(), 1 /* SW_SHOWNORMAL */);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);
    }
}
                                                                                                      