using System.IO;

namespace Win3muCore.NeFile
{
    public class ResourceEntry
    {
        public string name;
        public string nameTableName;
        public int offset;
        public int length;
        public ushort flags;
        public ushort id;
        public ushort handle;
        public ushort usage;

        public void Read(FileStream r)
        {
            offset = r.ReadUInt16();
            length = r.ReadUInt16();
            flags = r.ReadUInt16();
            id = r.ReadUInt16();
            handle = r.ReadUInt16();
            usage = r.ReadUInt16();
        }
    }
}
