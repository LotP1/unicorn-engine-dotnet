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

public struct ArmCpRegister
{
    public uint Cp;
    public uint Is64Bit;
    public uint Sec;
    public uint Crn;
    public uint Crm;
    public uint Opc1;
    public uint Opc2;
    public ulong Val;
}
#pragma warning restore CS0169 // Field is never used
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
