using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Win3muProxy
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                // Get location of Win3muCore.dll
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Win3mu");
                if (key== null)
                    throw new InvalidOperationException("Failed to open registry key");

                var location = key.GetValue("Location") as string;
                if (string.IsNullOrEmpty(location))
                    throw new InvalidOperationException("Can't locate Win3muCore.dll - please re-install");

                // Find the Run method
                var core = Assembly.LoadFrom(System.IO.Path.Combine(location, "Win3muRuntime.dll"));
                var API = core.GetType("Win3muRuntime.API");
                var Run = (Func<string, string[], int, int>)API.GetMethod("GetRunMethod").Invoke(null, null);

                // Work out params

                var program = System.IO.Path.ChangeExtension(typeof(Program).Assembly.Location, ".exe16");
                var showWindow = 1;

                // Run it
                return Run(program, args, showWindow);
            }
            catch (Exception x)
            {
                MessageBox(IntPtr.Zero, x.Message, "Win3mu Failed", 0x10);
                return 7;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);
    }
}
