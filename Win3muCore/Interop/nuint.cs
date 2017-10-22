using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct nuint : IFormattable
    {
        public nuint(uint value)
        {
            this.value = value;
        }

        public uint value;

        public static nuint To32(ushort value)
        {
            return new nuint(value);
        }

        public static ushort To16(nuint value)
        {
            return (ushort)value.value;
        }

        public static implicit operator nuint(uint value)
        {
            return new nuint(value);
        }

        public static implicit operator uint (nuint value)
        {
            return value.value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return value.ToString(format, formatProvider);
        }
    }
}
