using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public static class RegisteredWindowMessages
    {
        static HashSet<ushort> _registeredWindowMessages = new HashSet<ushort>();
        public static void Register(uint message)
        {
            if (message <= 0xFFFF)
            {
                _registeredWindowMessages.Add(message.Loword());
            }
            else
            {
                throw new NotImplementedException(string.Format("registered window message outside 16-bit range - {0}", message));
            }
        }

        public static bool IsRegistered(ushort message)
        {
            return _registeredWindowMessages.Contains(message);
        }

    }
}
