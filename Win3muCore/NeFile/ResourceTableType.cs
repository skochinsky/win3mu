using System;
using System.Collections.Generic;
using System.IO;

namespace Win3muCore.NeFile
{
    public class ResourceTypeTable
    {
        public string name;
        public ushort type;
        public ResourceEntry[] resources;

        public ResourceTypeTable(FileStream r)
        {
            resources = null;
            name = null;

            // Read the type
            type = r.ReadUInt16();
            if (type == 0)
                return;

            // Read the count
            int count = r.ReadUInt16();

            // Reserved
            r.ReadUInt16();
            r.ReadUInt16();

            // Allocate resource entries
            resources = new ResourceEntry[count];
            for (int i = 0; i < count; i++)
            {
                resources[i] = new ResourceEntry();
                resources[i].Read(r);
            }
        }

        Dictionary<string, ResourceEntry> _entryMap;
        public ResourceEntry FindEntry(string name)
        {
            if (_entryMap == null)
            {
                _entryMap = new Dictionary<string, ResourceEntry>(StringComparer.InvariantCultureIgnoreCase);
                foreach (var e in resources)
                {
                    if (!_entryMap.ContainsKey(e.name))
                    {
                        _entryMap.Add(e.name, e);
                    }
                    if (e.nameTableName != null && !_entryMap.ContainsKey(e.nameTableName))
                        _entryMap.Add(e.nameTableName, e);
                }
            }

            ResourceEntry re;
            if (!_entryMap.TryGetValue(name, out re))
                return null;

            return re;
        }
    }

}
