using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore.NeFile
{
    public enum TargetOS : byte
    {
        Unknown,    //Obvious ;)
        OS2,        //OS/2 (as if you hadn't worked that out!)
        Win,        //Windows (Win16)
        Dos4,       //European DOS  4.x
        Win386,     //Windows for the 80386 (Win32s). 32 bit code.
        BOSS        //The boss, a.k.a Borland Operating System Services
    };

}
