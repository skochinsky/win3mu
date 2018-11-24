/*
Win3mu - Windows 3 Emulator
Copyright (C) 2017 Topten Software.

Win3mu is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Win3mu is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Win3mu.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Diagnostics;

namespace Win3muCore.NeFile
{
    public class RelocationEntry
    {
        public RelocationEntry(FileStream r)
        {
            addressType = (RelocationAddressType)r.ReadByte();
            type = (RelocationType)r.ReadByte();
            offset = r.ReadUInt16();
            param1 = r.ReadUInt16();
            param2 = r.ReadUInt16();
        }

        public RelocationAddressType addressType;
        public RelocationType type;
        public ushort offset;
        public ushort param1;
        public ushort param2;

        public string TypeString
        {
            get
            {
                if ((type & RelocationType.Additive) == 0)
                    return type.ToString();
                else
                    return ((RelocationType)(type & ~RelocationType.Additive)).ToString() + " | Additive";
            }
        }
        class  fpRelocConsts
        {
            const ushort fINT = 0xCD;
            public const ushort fFWAIT = 0x9B;
            public const ushort fESCAPE = 0xD8;
            public const ushort fFNOP = 0x90;
            const ushort fES = 0x26;
            const ushort fCS = 0x2E;
            const ushort fSS = 0x36;
            const ushort fDS = 0x3E;
            const ushort BEGINT = 0x34;
            // below constants are defined in a way so that adding them to the FPU opcode
            // produces a valid FPU emulator interrupt call
            public const ushort FIARQQ = ((fINT + 256 * (BEGINT + 8)) - (fFWAIT + 256 * fDS))&0xFFFF;
            public const ushort FISRQQ = (fINT + 256 * (BEGINT + 8)) - (fFWAIT + 256 * fSS);
            public const ushort FICRQQ = (fINT + 256 * (BEGINT + 8)) - (fFWAIT + 256 * fCS);
            public const ushort FIERQQ = (fINT + 256 * (BEGINT + 8)) - (fFWAIT + 256 * fES);
            public const ushort FIDRQQ = ((fINT + 256 * (BEGINT + 0)) - (fFWAIT + 256 * fESCAPE))&0xFFFF;
            public const ushort FIWRQQ = ((fINT + 256 * (BEGINT + 9)) - (fFNOP + 256 * fFWAIT))&0xFFFF;
            public const ushort FJARQQ = (256 * (((0 << 6) | (fESCAPE & 0x3F)) - fESCAPE)) & 0xFFFF;
            public const ushort FJSRQQ = (256 * (((1 << 6) | (fESCAPE & 0x3F)) - fESCAPE)) & 0xFFFF;
            public const ushort FJCRQQ = (256 * (((2 << 6) | (fESCAPE & 0x3F)) - fESCAPE)) & 0xFFFF;
        }
        public ushort getOsFixupAddend(bool word0)
        {
            System.Diagnostics.Debug.Assert((RelocationType)(type & ~RelocationType.Additive) == RelocationType.OSFixUp);
            switch (param1)
            {
                case 0:
                    return word0 ? fpRelocConsts.FIARQQ : fpRelocConsts.FJARQQ;
                case 1:
                    return word0 ? fpRelocConsts.FISRQQ : fpRelocConsts.FJSRQQ;
                case 2:
                    return word0 ? fpRelocConsts.FICRQQ : fpRelocConsts.FJCRQQ;
                case 3:
                    return word0 ? fpRelocConsts.FIERQQ : (ushort)0;
                case 4:
                    return word0 ? fpRelocConsts.FIDRQQ : (ushort)0;
                case 5:
                case 11:
                    return word0 ? fpRelocConsts.FIWRQQ : (ushort)0;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    return word0 ? (ushort)((fpRelocConsts.fFNOP - fpRelocConsts.fFWAIT)&0xFFFF) : (ushort)0;
                default:
                    throw new NotImplementedException(string.Format("Don't know how to apply OS Fixup for FP operation at {0:X4} {1:X2} {2:X2}   [p1={3}, p2={4}]", offset, param1, param2));

            }

        }
    }
}

