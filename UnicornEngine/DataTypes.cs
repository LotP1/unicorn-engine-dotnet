using UnicornEngine.Const;

namespace UnicornEngine;

struct TlbEntry
{
    public ulong PAddr;
    public UcProtection Perms;
}

struct TranslationBlock
{
    public nint Pc;
    public short ICount;
    public short Size;
}

struct MemRegion
{
    nint Begin;
    nint End;
    UcProtection Perms;
}