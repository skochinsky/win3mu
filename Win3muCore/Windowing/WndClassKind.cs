using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public enum WndClassKind
    {
        Standard,
        Unknown,
        Dialog,
        Button,
        Edit,
        Static,
        Listbox,
        Scrollbar,
        Combobox,
        Count,
    }

    public static class WindowClassKind
    {
        static Dictionary<string, WndClassKind> _mapClassNameClassKind = new Dictionary<string, WndClassKind>()
        {
            { "#32770", WndClassKind.Dialog },
            { "button", WndClassKind.Button },
            { "edit", WndClassKind.Edit },
            { "static", WndClassKind.Static },
            { "listbox", WndClassKind.Listbox },
            { "scrollbar", WndClassKind.Scrollbar },
            { "combobox", WndClassKind.Combobox },
        };

        public static WndClassKind Get(HWND hWnd)
        {
            // Get the class name
            var className = User.GetClassName(hWnd).ToLowerInvariant();

            // Known class?
            WndClassKind kind;
            if (_mapClassNameClassKind.TryGetValue(className, out kind))
            {
                return kind;
            }

            return WndClassKind.Unknown;
        }

        // This method is called whenever CallWindowProc is called with WM_NCCREATE
        // Use it to detect the window kind for superclasses
        public static void DetectSuperClasses(HWND hWnd, IntPtr WndProc32)
        {
            // Get the class name
            var className = User.GetClassName(hWnd);

            // Already known?
            if (_mapClassNameClassKind.ContainsKey(className))
                return;

            // Check the existing known class names
            Win32.get_WNDCLASS wc;
            foreach (var vk in _mapClassNameClassKind)
            {
                User.GetClassInfo(IntPtr.Zero, vk.Key, out wc);
                if (wc.lpfnWndProc == WndProc32)
                {
                    _mapClassNameClassKind.Add(className.ToLowerInvariant(), vk.Value);
                    return;
                }
            }
        }
    }
}
