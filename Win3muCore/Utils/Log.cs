using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public static class Log
    {                                    
        static TextWriter output1 = null;
        static TextWriter output2 = null;

        public static void Init(bool console, string outputFile)
        {
            if (console)
                output1 = Console.Out;

            if (outputFile != null)
                output2 = new StreamWriter(outputFile, false, Encoding.UTF8);
        }

        public static void Close()
        {
            if (output2 != null)
            {
                output2.Close();
                output2.Dispose();
            }
        }

        public static void Flush()
        {
            if (output2 != null)
                output2.Flush();
        }

        public static void Write(string str)
        {
            if (output1 != null)
                output1.Write(str);
            if (output2 != null)
                output2.Write(str);
        }

        public static void WriteLine(string str="")
        {
            if (output1 != null)
                output1.WriteLine(str);
            if (output2 != null)
                output2.WriteLine(str);
        }

        public static void WriteLine(string str, params object[] args)
        {
            if (output1 != null)
                output1.WriteLine(string.Format(str, args));
            if (output2 != null)
                output2.WriteLine(string.Format(str, args));
        }

        public static void Write(string str, params object[] args)
        {
            if (output1 != null)
                output1.Write(string.Format(str, args));
            if (output2 != null)
                output2.Write(string.Format(str, args));
        }
    }
}
