namespace Win3muCore.NeFile
{
    public enum RelocationAddressType : byte
    {
        LowByte = 0,
        Selector = 2,
        Pointer32 = 3,
        Offset16 = 5,
        Pointer48 = 11,
        Offset32 = 13,
    }

}
