namespace Win3muCore.NeFile
{
    public enum RelocationType : byte
    {
        InternalReference = 0,      // If segment fixed:    p1 = segment number, p2 = segment offset
                                    // If segment moveable: p1 = 0xff p2 = ordinal in segment entry table
        ImportedOrdinal = 1,        // p1 = module index, p2 = ordinal
        ImportedName = 2,           // p1 = module index, p2 = offset to imported name table
        OSFixUp = 3,

        Additive = 0x04,
    }

}
