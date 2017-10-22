namespace Win3muCore.NeFile
{
    public class EntryPoint
    {
        public ushort ordinal;
        public byte flags;
        public byte segmentNumber;
        public ushort segmentOffset;

        public const byte FLAG_EXPORTED = 0x01;
        public const byte FLAG_SHAREDDS = 0x02;
    }

}
