using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp86;

namespace Win3muCore
{
    public class StringOrId
    {
        public StringOrId(ushort id)
        {
            ID = id;
        }

        public StringOrId(string str)
        {
            Name = str;
        }

        public StringOrId(Machine machine, uint lpszName)
        {
            if (lpszName.Hiword() == 0)
            {
                ID = lpszName.Loword();
            }
            else
            {
                Name = machine.MemoryBus.ReadString(lpszName);
            }
        }

        public string Name;
        public ushort ID;

        public bool IsNull
        {
            get { return Name == null && ID == 0; }
        }

        public override string ToString()
        {
            if (Name != null)
                return Name;
            else
                return string.Format("#{0}", ID);
        }
    }
}
