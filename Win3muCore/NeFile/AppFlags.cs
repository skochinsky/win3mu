using System;

namespace Win3muCore.NeFile
{
    [Flags]
    public enum AppFlags : byte
    {
        None = 0,
        FullScreeen = 1,    //fullscreen (not aware of Windows/P.M. API)
        WinPMCompat = 2,    //compatible with Windows/P.M. API
        WinPMUses = 3,      //uses Windows/P.M. API

        OS2APP = 1 << 3,          //OS/2 family application
        IMAGEERROR = 1 << 5,      //errors in image/executable
        NONCONFORM = 1 << 6,      //non-conforming program?
        DLL = 1 << 7,             //DLL or driver (SS:SP invalid, CS:IP->Far INIT routine AX=HMODULE,returns AX==0 success, AX!=0 fail)
    };
}
