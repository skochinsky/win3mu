using System;

namespace Win3muCore.NeFile
{
    [Flags]
    public enum SegmentFlags : ushort
    {
        Data = 1 << 0,
        Allocated = 1 << 1,
        Loaded = 1 << 2,
        Moveable = 1 << 4,
        Pure = 1 << 5,          // ie: Shareable
        Preload = 1 << 6,
        ReadOnly = 1 << 7,
        HasRelocations = 1 << 8,
        Discardable = 1 << 12,
    }
}
