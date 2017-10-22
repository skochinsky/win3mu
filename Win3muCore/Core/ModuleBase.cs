using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public abstract class ModuleBase
    {
        public abstract string GetModuleName();
        public abstract string GetModuleFileName();
        public abstract void Load(Machine machine);
        public abstract void Unload(Machine machine);
        public abstract IEnumerable<string> GetReferencedModules();
        public abstract void Link(Machine machine);
        public abstract void Init(Machine machine);
        public abstract void Uninit(Machine machine);
        public abstract ushort GetOrdinalFromName(string functionName);
        public abstract string GetNameFromOrdinal(ushort ordinal);
        public abstract uint GetProcAddress(ushort ordinal);
        public abstract IEnumerable<ushort> GetExports();

        public int LoadCount;
        public bool Initialized;
        public ushort hModule;
    }
}
