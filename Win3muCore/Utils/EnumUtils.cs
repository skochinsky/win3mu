using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public static class EnumUtils
    {
        public static string FormatFlags<T>(T value)
        {
            var intVal = (int)Convert.ChangeType(value, typeof(int));
            return FormatFlags(typeof(T), intVal);
        }

        public static string FormatFlags(Type type, int value)
        {
            var values = Enum.GetValues(type);
            var sb = new StringBuilder();

            foreach (var vo in values)
            {
                int mask = (int)Convert.ChangeType(vo, typeof(int));
                if ((value & mask)!=0)
                {
                    if (sb.Length>0)
                    {
                        sb.Append(" | ");
                    }
                    sb.Append(Enum.GetName(type, vo));

                    value = value & ~((int)mask);
                }
            }

            if (value != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" | ");
                sb.AppendFormat("0x{0:X}", value);
            }

            return sb.ToString();
        }
    }
}
