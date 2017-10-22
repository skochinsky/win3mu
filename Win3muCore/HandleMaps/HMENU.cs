using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HMENU
    {
        public IntPtr value;

        public static implicit operator HMENU(IntPtr value) { return new HMENU() { value = value }; }
        public static HMENU Null = new HMENU() { value = IntPtr.Zero };
        public static HandleMap Map = new HandleMap();
        public static HMENU To32(ushort hMenu) { return new HMENU() { value = Map.To32(hMenu) }; }
        public static ushort To16(HMENU hMenu) { return Map.To16(hMenu.value); }
        public static void Destroy(ushort hMenu) { Map.Destroy16(hMenu); }
        public static void Trim()
        {
            foreach (var x in Map.GetAll32().Where(x => !IsMenu(x)).ToList())
            {
                Map.Destroy32(x);
            }
        }
        static bool IsMenu(IntPtr hMenu)
        {
            if (hMenu == IntPtr.Zero)
                return false;
            int count = User.GetMenuItemCount(hMenu);
            return count >= 0;
        }
    }
}
