using System.IO;

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
    }
}
