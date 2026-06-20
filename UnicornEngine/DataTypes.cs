#pragma warning disable CS0169 // Field is never used
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
using UnicornEngine.Const;

namespace UnicornEngine;

public struct TlbEntry
{
    public ulong PAddr;
    public UcProtection Perms;
}

public struct TranslationBlock
{
    public nint Pc;
    public short InstructionCount;
    public short Size;
}

public struct MemRegion
{
    public nint Begin;
    public nint End;
    public UcProtection Perms;
}

public struct ContextObj
{
    private nint _context;

    public bool IsAllocated() => _context != 0;
}
#pragma warning restore CS0169 // Field is never used
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
