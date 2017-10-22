using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    [MappedTypeAttribute]
    public struct HWND
    {
        public IntPtr value;

        public static implicit operator HWND(IntPtr value)
        {
            return new HWND() { value = value };
        }

        public static HandleMap Map = new HandleMap();
        public static HWND Null = IntPtr.Zero;




        public static HWND To32(ushort hWnd)
        {
            // Trim needed?
            if (_destroyDepth == 0 && _needsTrim)
                Trim();

            // Get the handle
            var hWnd32 = Map.To32(hWnd);

            // If we're destroying, then check it
            if (_destroyDepth > 0 && !User.IsWindow(hWnd32))
                return Null;

            return new HWND() { value = hWnd32 };
        }

        public static ushort To16(HWND hWnd)
        {
            // If destroying then check it
            if (_destroyDepth > 0 && !User.IsWindow(hWnd.value))
            {
                return 0;
            }

            // Trim if needed
            if (_needsTrim && _destroyDepth == 0)
            {
                Trim();
            }

            // Map it
            return Map.To16(hWnd.value);
        }

        public static void Destroy(ushort hWnd)
        {
            _needsTrim = true;
        }

        public override string ToString()
        {
            return string.Format("HWND(0x{0:X}/0x{1:X})", Map.To16(value), (ulong)value.ToInt64());
        }

        static int _destroyDepth;
        static bool _needsTrim;

        public static void EnterDestroy()
        {
            _destroyDepth++;
        }

        public static void LeaveDestroy()
        {
            _destroyDepth--;
            if (_destroyDepth == 0)
            {
                _needsTrim = true;
            }
        }

        public static void Trim()
        {
            // Trim invalid window handles
            foreach (var w in Map.GetAll32().Where(x => !User.IsWindow(x)).ToList())
            {
                Map.Destroy32(w);
            }

            // Also trim menu map
            HMENU.Trim();

            // Clear trim flag
            if (_destroyDepth == 0)
                _needsTrim = false;
        }

        static Dictionary<IntPtr, ushort> _hWndInstMap = new Dictionary<IntPtr, ushort>();
        public static void RegisterHWndToHInstance(IntPtr hWnd, ushort hInst)
        {
            _hWndInstMap[hWnd] = hInst;
        }

        public static ushort HInstanaceOfHWnd(IntPtr hWnd)
        {
            ushort hInst;
            if (_hWndInstMap.TryGetValue(hWnd, out hInst))
                return hInst;
            return 0;
        }
    }

}
