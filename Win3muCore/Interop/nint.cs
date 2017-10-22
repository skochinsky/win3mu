using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedType]
    public struct nint : IFormattable
    {
        public nint(int value)
        {
            this.value = value;
        }

        public int value;

        public static nint To32(short value)
        {
            return new nint(value);
        }

        public static short To16(nint value)
        {
            return (short)value.value;
        }

        public static implicit operator nint(int value)
        {
            return new nint(value);
        }

        public static implicit operator int(nint value)
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
