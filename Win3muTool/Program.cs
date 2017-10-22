using System;
using System.Runtime.InteropServices;
using Win3muRuntime;

namespace Win3muTool
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
                return 0;

            try
            {
                if (args.Length > 0 && args[0] == "/register")
                {
                    API.Register();
                    return 0;
                }
                if (args.Length > 0 && args[0] == "/unregister")
                {
                    API.Unregister();
                    return 0;
                }

                // Get location of proxy
                var sourceStub = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location), "Win3muProxy.exe");
                if (!System.IO.File.Exists(sourceStub))
                {
                    throw new System.IO.FileNotFoundException(string.Format("Win3mu stub exe not found.\n\n{0}", sourceStub));
                }

                // Get the exe we're repacking
                var sourceExe = args[0];

                // Check it exists
                if (!System.IO.File.Exists(sourceExe))
                {
                    throw new System.IO.FileNotFoundException(string.Format("16-bit executable to be upgraded not found.\n\n{0}", sourceExe));
                }

                // Build it
                API.BuildStub(sourceStub, sourceExe, sourceExe, true);

                return 0;
            }
            catch (Exception x)
            {
                MessageBox(IntPtr.Zero, x.Message, "Win3mu", 0x10);
                return 7;
            }
        }


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);
    }
}
