using System.IO;

namespace Win3muCore.NeFile
{
    public class SegmentEntry
    {
        public int offset;
        public ushort lengthBytes;
        public ushort allocationBytes;
        public SegmentFlags flags;
        public RelocationEntry[] relocations;

        public ushort globalHandle;

        public void Read(FileStream f)
        {
            offset = f.ReadUInt16();
            lengthBytes = f.ReadUInt16();
            flags = (SegmentFlags)f.ReadUInt16();
            allocationBytes = f.ReadUInt16();
        }
    }
}
