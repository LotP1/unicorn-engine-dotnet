using System.Runtime.InteropServices;
using UnicornEngine.Const;

namespace UnicornEngine;

public class Unicorn
{
    private nint _engine;

    private readonly List<Hook> _hooks = new();

    public static void Version(out uint major, out uint minor, out uint patch, out uint extra)
    {
        major = 0;
        minor = 0;

        uint version = Bindings.Version(ref major, ref minor);

        extra = version & 0xff;
        patch = (version >> 8) & 0xff;
    }

    public static void Version(out uint major, out uint minor, out uint patch)
    {
        major = 0;
        minor = 0;

        uint version = Bindings.Version(ref major, ref minor);

        patch = (version >> 8) & 0xff;
    }

    public static bool ArchSupported(UcArch arch)
    {
        return Bindings.ArchSupported(arch);
    }

    public void Open(UcArch arch, UcMode mode)
    {
        if (_engine != 0)
        {
            ThrowOnError(UcError.Handle);
        }

        ThrowOnError(Bindings.Open(arch, mode, ref _engine));
    }

    public void Close()
    {
        AssertEngine();

        ThrowOnError(Bindings.Close(_engine));

        _engine = 0;
    }

    #region query
    public UcMode QueryMode()
    {
        AssertEngine();

        UcMode mode = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Mode, new nint(&mode)));
        }

        return mode;
    }

    public uint QueryPageSize()
    {
        AssertEngine();

        uint pageSize = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.PageSize, new nint(&pageSize)));
        }

        return pageSize;
    }

    public UcArch QueryArch()
    {
        AssertEngine();

        UcArch arch = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Arch, new nint(&arch)));
        }

        return arch;
    }

    public bool QueryTimeout()
    {
        AssertEngine();

        bool timeout = false;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Timeout, new nint(&timeout)));
        }

        return timeout;
    }
    #endregion

    #region ctl
    public void GetMode(out UcMode mode)
    {
        AssertEngine();

        unsafe
        {
            fixed (UcMode* ptr = &mode)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.UcMode, 1), new nuint(ptr)));
            }
        }

    }

    public void GetPageSize(out uint size)
    {
        AssertEngine();

        unsafe
        {
            fixed (uint* ptr = &size)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.UcPageSize, 1), new nuint(ptr)));
            }
        }
    }

    public void SetPageSize(uint size)
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.UcPageSize, 1), size));
    }

    public void GetArch(out UcArch arch)
    {
        AssertEngine();

        unsafe
        {
            fixed (UcArch* ptr = &arch)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.UcArch, 1), new nuint(ptr)));
            }
        }
    }

    public void GetTimeout(out ulong timeout)
    {
        AssertEngine();

        unsafe
        {
            fixed (ulong* ptr = &timeout)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.UcTimeout, 1), new nuint(ptr)));
            }
        }
    }

    public void EnableExits()
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.UcUseExits, 1), 1));
    }

    public void DisableExits()
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.UcUseExits, 1), 0));
    }

    public void GetExitCount(out ulong exitCount)
    {
        AssertEngine();

        unsafe
        {
            fixed (ulong* ptr = &exitCount)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.UcExitsCnt, 1), new nuint(ptr)));
            }
        }
    }

    public void GetExits(out nint[] exits)
    {
        AssertEngine();

        GetExitCount(out ulong exitCount);

        exits = new nint[exitCount];

        unsafe
        {
            fixed (nint* ptr = &exits[0])
            {
                ThrowOnError(Bindings.CtlArg2(_engine, CtlRead(UcControlType.UcExits, 2), new nuint(ptr), exitCount));
            }
        }
    }

    public void SetExits(nint[] exits)
    {
        AssertEngine();

        unsafe
        {
            fixed (nint* ptr = &exits[0])
            {
                ThrowOnError(Bindings.CtlArg2(_engine, CtlWrite(UcControlType.UcExits, 2), new nuint(ptr), (ulong)exits.Length));
            }
        }
    }

    #region GetCpu
    public void GetCpuModel(out CpuArm cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuArm* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuArm64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuArm64* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuM68k cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuM68k* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuMips32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Mips || (mode & UcMode.Mips32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuMips32* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuMips64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Mips || (mode & UcMode.Mips64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuMips64* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuPpc32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Ppc || (mode & UcMode.Ppc32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuPpc32* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuPpc64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Ppc || (mode & UcMode.Ppc64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuPpc64* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuRiscv32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Riscv || (mode & UcMode.Riscv32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuRiscv32* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuRiscv64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Riscv || (mode & UcMode.Riscv64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuRiscv64* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuS390x cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuS390x* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuSparc32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Sparc || (mode & UcMode.Sparc32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuSparc32* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuSparc64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Sparc || (mode & UcMode.Sparc64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuSparc64* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuTricore cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuTricore* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }

    public void GetCpuModel(out CpuX86 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (CpuX86* ptr = &cpu)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.CpuModel, 1), new nuint(ptr)));
            }
        }
    }
    #endregion

    #region SetCpu
    public void SetCpuModel(CpuArm cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuArm64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuM68k cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuMips32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Mips || (mode & UcMode.Mips32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuMips64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Mips || (mode & UcMode.Mips64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuPpc32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Ppc || (mode & UcMode.Ppc32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuPpc64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Ppc || (mode & UcMode.Ppc64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuRiscv32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Riscv || (mode & UcMode.Riscv32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuRiscv64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Riscv || (mode & UcMode.Riscv64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuS390x cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuSparc32 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Sparc || (mode & UcMode.Sparc32) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuSparc64 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);
        GetMode(out UcMode mode);

        if (arch != UcArch.Sparc || (mode & UcMode.Sparc64) == 0)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuTricore cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }

    public void SetCpuModel(CpuX86 cpu)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.CpuModel, 1), (ulong)cpu));
    }
    #endregion

    public void TbRequestCache(nint address, out TranslationBlock block)
    {
        AssertEngine();

        unsafe
        {
            fixed (TranslationBlock* ptr = &block)
            {
                ThrowOnError(Bindings.CtlArg2(_engine, CtlRead(UcControlType.TbRequestCache, 2), new nuint(address.ToPointer()), new nuint(ptr)));
            }
        }
    }

    public void TbRemoveCache(nint address, nint end)
    {
        AssertEngine();

        unsafe
        {
            ThrowOnError(Bindings.CtlArg2(_engine, CtlWrite(UcControlType.TbRemoveCache, 2), new nuint(address.ToPointer()), new nuint(end.ToPointer())));
        }
    }

    public void FlushTb()
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg0(_engine, CtlWrite(UcControlType.TbFlush, 0)));
    }

    public void FlushTlb()
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg0(_engine, CtlWrite(UcControlType.TlbFlush, 0)));
    }

    public void SetTlbMode(UcTlbType mode)
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.TlbType, 1), (uint)mode));
    }

    public void GetTcgBufferSize(out uint size)
    {
        AssertEngine();

        unsafe
        {
            fixed (uint* ptr = &size)
            {
                ThrowOnError(Bindings.CtlArg1(_engine, CtlRead(UcControlType.TcgBufferSize, 1), new nuint(ptr)));
            }
        }
    }

    public void SetGetTcgBufferSize(uint size)
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.TcgBufferSize, 1), size));
    }

    public void SetContextMode(UcControlContentType mode)
    {
        AssertEngine();

        ThrowOnError(Bindings.CtlArg1(_engine, CtlWrite(UcControlType.TcgBufferSize, 1), (uint)mode));
    }
    #endregion

    public UcError LastError()
    {
        AssertEngine();

        return Bindings.ErroNo(_engine);
    }

    public static string ErrorString(UcError err)
    {
        unsafe
        {
            return new string(Bindings.StrError(err));
        }
    }

    #region RegWrite
    public void CpRegWrite(ArmCpRegister value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            ArmCpRegister* ptr = &value;
            ThrowOnError(Bindings.RegWriteCpReg(_engine, RegArm.CpReg, ptr));
        }
    }

    public void RegWrite(RegArm reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegArm64 reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegM68k reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegMips reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegPpc reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegRiscv reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegS390x reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegSparc reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegTricore reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegWrite(RegX86 reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegWrite(_engine, (uint)reg, ptr));
            }
        }
    }
    #endregion

    #region RegRead 
    public void CpRegRead(ref ArmCpRegister value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (ArmCpRegister* ptr = &value)
            {
                ThrowOnError(Bindings.RegReadCpReg(_engine, RegArm.CpReg, ptr));
            }
        }
    }

    public void RegRead(RegArm reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegArm64 reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegM68k reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegMips reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegPpc reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegRiscv reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegS390x reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegSparc reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegTricore reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }

    public void RegRead(RegX86 reg, Span<byte> value)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.RegRead(_engine, (uint)reg, ptr));
            }
        }
    }
    #endregion

    #region RegWriteBatch
    public void RegWriteBatch(Span<RegArm> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegArm64> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegM68k> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegMips> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegPpc> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegRiscv> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegS390x> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegSparc> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegTricore> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegWriteBatch(Span<RegX86> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegWrite(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }
    #endregion

    #region RegReadBatch
    public void RegReadBatch(Span<RegArm> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegArm64> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegM68k> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegMips> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegPpc> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegRiscv> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegS390x> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegSparc> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegTricore> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void RegReadBatch(Span<RegX86> regs, Span<byte[]> values)
    {
        AssertEngine();

        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.RegRead(_engine, (uint)regs[i], ptr));
                }
            }
        }
    }
    #endregion

    public void MemWrite(nint address, Span<byte> data)
    {
        AssertEngine();

        unsafe
        {
            fixed (byte* ptr = data)
            {
                ThrowOnError(Bindings.MemWrite(_engine, address, ptr, (ulong)data.Length));
            }
        }
    }

    public void MemRead(nint address, ulong length, out Span<byte> data)
    {
        AssertEngine();

        data = new byte[length];

        unsafe
        {
            fixed (byte* ptr = data)
            {
                ThrowOnError(Bindings.MemRead(_engine, address, ptr, (ulong)data.Length));
            }
        }
    }

    public void VMemWrite(nint vAddress, UcProtection protection, Span<byte> data)
    {
        AssertEngine();

        unsafe
        {
            fixed (byte* ptr = data)
            {
                ThrowOnError(Bindings.VMemWrite(_engine, vAddress, protection, ptr, (ulong)data.Length));
            }
        }
    }

    public void VMemRead(nint vAddress, ulong length, UcProtection protection, out Span<byte> data)
    {
        AssertEngine();

        data = new byte[length];

        unsafe
        {
            fixed (byte* ptr = data)
            {
                ThrowOnError(Bindings.VMemRead(_engine, vAddress, protection, ptr, (ulong)data.Length));
            }
        }
    }

    public void VMemTranslate(nint vAddress, UcProtection protection, out nint address)
    {
        AssertEngine();

        address = 0;

        ThrowOnError(Bindings.VMemTranslate(_engine, vAddress, protection, ref address));
    }

    public void StartEmulation(nint beginAddress, nint endAddress, ulong timeout, ulong count)
    {
        AssertEngine();

        ThrowOnError(Bindings.EmuStart(_engine, beginAddress, endAddress, timeout, count));
    }

    public void StopEmulation()
    {
        AssertEngine();

        ThrowOnError(Bindings.EmuStop(_engine));
    }

    #region HookAdd
    public void HookInterrupt(nint beginAddress, nint endAddress, InterruptHook.CallbackDelegate callback, byte[] userData, out InterruptHook result)
    {
        AssertEngine();

        result = new InterruptHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.Intr, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookInInstruction(nint beginAddress, nint endAddress, InHook.CallbackDelegate callback, byte[] userData, out InHook result)
    {
        AssertEngine();

        result = new InHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAdd1Arg(_engine, ref result.HookHandle, UcHookType.Insn, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress, (ulong)InstructionX86.In));

        _hooks.Add(result);
    }

    public void HookOutInstruction(nint beginAddress, nint endAddress, OutHook.CallbackDelegate callback, byte[] userData, out OutHook result)
    {
        AssertEngine();

        result = new OutHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAdd1Arg(_engine, ref result.HookHandle, UcHookType.Insn, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress, (ulong)InstructionX86.Out));

        _hooks.Add(result);
    }

    public void HookSyscallInstruction(nint beginAddress, nint endAddress, SyscallHook.CallbackDelegate callback, byte[] userData, out SyscallHook result)
    {
        AssertEngine();

        result = new SyscallHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAdd1Arg(_engine, ref result.HookHandle, UcHookType.Insn, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress, (ulong)InstructionX86.Syscall));

        _hooks.Add(result);
    }

    public void HookCpuIdInstruction(nint beginAddress, nint endAddress, CpuIdHook.CallbackDelegate callback, byte[] userData, out CpuIdHook result)
    {
        AssertEngine();

        result = new CpuIdHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAdd1Arg(_engine, ref result.HookHandle, UcHookType.Insn, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress, (ulong)InstructionX86.Syscall));

        _hooks.Add(result);
    }

    public void HookCode(nint beginAddress, nint endAddress, CodeHook.CallbackDelegate callback, byte[] userData, out CodeHook result)
    {
        AssertEngine();

        result = new CodeHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.Code, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookBlock(nint beginAddress, nint endAddress, CodeHook.CallbackDelegate callback, byte[] userData, out CodeHook result)
    {
        AssertEngine();

        result = new CodeHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.Block, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemReadUnmapped(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemReadUnmapped, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemWriteUnmapped(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemWriteUnmapped, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemFetchUnmapped(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemFetchUnmapped, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemUnmapped(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemUnmapped, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemReadProtected(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemReadProt, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemWriteProtected(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemWriteProt, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemFetchProtected(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemFetchProt, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemProtected(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemProt, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemReadInvalid(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemReadInvalid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemWriteInvalid(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemWriteInvalid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemFetchInvalid(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemFetchInvalid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemInvalid(nint beginAddress, nint endAddress, InvalidMemHook.CallbackDelegate callback, byte[] userData, out InvalidMemHook result)
    {
        AssertEngine();

        result = new InvalidMemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemInvalid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemRead(nint beginAddress, nint endAddress, MemHook.CallbackDelegate callback, byte[] userData, out MemHook result)
    {
        AssertEngine();

        result = new MemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemRead, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemWrite(nint beginAddress, nint endAddress, MemHook.CallbackDelegate callback, byte[] userData, out MemHook result)
    {
        AssertEngine();

        result = new MemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemWrite, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemFetch(nint beginAddress, nint endAddress, MemHook.CallbackDelegate callback, byte[] userData, out MemHook result)
    {
        AssertEngine();

        result = new MemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemFetch, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMem(nint beginAddress, nint endAddress, MemHook.CallbackDelegate callback, byte[] userData, out MemHook result)
    {
        AssertEngine();

        result = new MemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemValid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookMemReadAfter(nint beginAddress, nint endAddress, MemHook.CallbackDelegate callback, byte[] userData, out MemHook result)
    {
        AssertEngine();

        result = new MemHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.MemReadAfter, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookInvalidInstruction(nint beginAddress, nint endAddress, InvalidInstructionHook.CallbackDelegate callback, byte[] userData, out InvalidInstructionHook result)
    {
        AssertEngine();

        result = new InvalidInstructionHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.InsnInvalid, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookEdgeGenerated(nint beginAddress, nint endAddress, EdgeGenHook.CallbackDelegate callback, byte[] userData, out EdgeGenHook result)
    {
        AssertEngine();

        result = new EdgeGenHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.EdgeGenerated, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }

    public void HookTchOpcode(nint beginAddress, nint endAddress, TcgOp2Hook.CallbackDelegate callback, byte[] userData, out TcgOp2Hook result, UcTcgOpcode opcode, UcTcgOpcodeFlag flag)
    {
        AssertEngine();

        result = new TcgOp2Hook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAdd2Args(_engine, ref result.HookHandle, UcHookType.TcgOpcode, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress, (ulong)opcode, (ulong)flag));

        _hooks.Add(result);
    }

    public void HookTlbFill(nint beginAddress, nint endAddress, TlbHook.CallbackDelegate callback, byte[] userData, out TlbHook result)
    {
        AssertEngine();

        result = new TlbHook
        {
            Callback = callback,
            UserData = userData,
        };

        ThrowOnError(Bindings.HookAddNoArgs(_engine, ref result.HookHandle, UcHookType.TlbFill, Marshal.GetFunctionPointerForDelegate(result.InternalCallback), 0, beginAddress, endAddress));

        _hooks.Add(result);
    }
    #endregion

    public void HookDelete(Hook hook)
    {
        AssertEngine();

        int index = _hooks.FindIndex(h => h.HookHandle == hook.HookHandle);

        ThrowOnError(Bindings.HookDel(_engine, hook.HookHandle));

        _hooks.RemoveAt(index);
    }

    public void MemMap(nint address, ulong size, UcProtection perms)
    {
        AssertEngine();

        ThrowOnError(Bindings.MemMap(_engine, address, size, perms));
    }

    public void MemMapPtr(nint address, ulong size, UcProtection perms, nint ptr)
    {
        AssertEngine();

        ThrowOnError(Bindings.MemMapPtr(_engine, address, size, perms, ptr));
    }

    public void MmioMap(nint address, ulong size, MmioReadHook.CallbackDelegate readCallback, byte[] readUserData, MmioWriteHook.CallbackDelegate writeCallback, byte[] writeUserData)
    {
        AssertEngine();

        MmioReadHook readHook = new MmioReadHook
        {
            Callback = readCallback,
            UserData = readUserData,
        };

        MmioWriteHook writeHook = new MmioWriteHook
        {
            Callback = writeCallback,
            UserData = readUserData,
        };

        ThrowOnError(Bindings.MemIOMap(_engine, address, size, readHook.InternalCallback, 0, writeHook.InternalCallback, 0));

        _hooks.Add(readHook);
        _hooks.Add(writeHook);
    }

    public void MemUnmap(nint address, ulong size)
    {
        AssertEngine();

        ThrowOnError(Bindings.MemUnmap(_engine, address, size));
    }

    public void MemProtect(nint address, ulong size, UcProtection protection)
    {
        AssertEngine();

        ThrowOnError(Bindings.MemProtect(_engine, address, size, protection));
    }

    public void MemRegions(out Span<MemRegion> regions)
    {
        AssertEngine();

        uint count = 0;

        unsafe
        {
            MemRegion* ptr = null;

            ThrowOnError(Bindings.MemRegions(_engine, &ptr, ref count));

            regions = new Span<MemRegion>(ptr, (int)count);
        }
    }

    public void ContextAllocate(out ContextObj context)
    {
        AssertEngine();

        context = new ContextObj();

        unsafe
        {
            fixed (ContextObj* ptr = &context)
            {
                ThrowOnError(Bindings.ContextAlloc(_engine, ptr));
            }
        }
    }

    public void MemFree(ref MemRegion region)
    {
        AssertEngine();

        unsafe
        {
            fixed (MemRegion* ptr = &region)
            {
                ThrowOnError(Bindings.MemFree(ptr));
            }
        }
    }

    public void ContextSave(ref ContextObj context)
    {
        AssertEngine();

        if (!context.IsAllocated())
        {
            ThrowOnError(UcError.Handle);
        }

        ThrowOnError(Bindings.ContextSave(_engine, context));
    }

    #region ContextRegWrite 
    public void ContextRegWrite(ref ContextObj context, RegArm reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegArm64 reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegM68k reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegMips reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegPpc reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegRiscv reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegS390x reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegSparc reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegTricore reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegWrite(ref ContextObj context, RegX86 reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegWrite(context, (uint)reg, ptr));
            }
        }
    }
    #endregion

    #region ContextRegRead
    public void ContextRegRead(ref ContextObj context, RegArm reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegArm64 reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegM68k reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegMips reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegPpc reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegRiscv reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegS390x reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegSparc reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegTricore reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }

    public void ContextRegRead(ref ContextObj context, RegX86 reg, Span<byte> value)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            fixed (byte* ptr = value)
            {
                ThrowOnError(Bindings.ContextRegRead(context, (uint)reg, ptr));
            }
        }
    }
    #endregion

    #region ContextRegWriteBatch
    public void ContextRegWriteBatch(ref ContextObj context, Span<RegArm> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegArm64> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegM68k> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegMips> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegPpc> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegRiscv> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegS390x> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegSparc> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegTricore> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegWriteBatch(ref ContextObj context, Span<RegX86> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegWrite(context, (uint)regs[i], ptr));
                }
            }
        }
    }
    #endregion

    #region ContextRegReadBatch
    public void ContextRegReadBatch(ref ContextObj context, Span<RegArm> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegArm64> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Arm64)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegM68k> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.M68k)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegMips> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Mips)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegPpc> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Ppc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegRiscv> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Riscv)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegS390x> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.S390x)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegSparc> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Sparc)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegTricore> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.Tricore)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }

    public void ContextRegReadBatch(ref ContextObj context, Span<RegX86> regs, Span<byte[]> values)
    {
        GetArch(out UcArch arch);

        if (arch != UcArch.X86)
        {
            ThrowOnError(UcError.Arg);
        }

        unsafe
        {
            for (int i = 0; i < regs.Length; i++)
            {
                fixed (byte* ptr = &values[i][0])
                {
                    ThrowOnError(Bindings.ContextRegRead(context, (uint)regs[i], ptr));
                }
            }
        }
    }
    #endregion


    public void ContextRestore(ref ContextObj context)
    {
        AssertEngine();

        if (!context.IsAllocated())
        {
            ThrowOnError(UcError.Handle);
        }

        ThrowOnError(Bindings.ContextRestore(_engine, context));
    }

    public ulong GetContextSize()
    {
        AssertEngine();

        return Bindings.ContextSize(_engine);
    }

    public void ContextFree(ref ContextObj context)
    {
        if (!context.IsAllocated())
        {
            ThrowOnError(UcError.Handle);
        }

        ThrowOnError(Bindings.ContextFree(context));

        context = new ContextObj();
    }

    #region helpers
    private static uint CtlWrite(UcControlType type, uint count) => (uint)type | (count << 26) | (1u << 30);

    private static uint CtlRead(UcControlType type, uint count) => (uint)type | (count << 26) | (2u << 30);

    private static void ThrowOnError(UcError err)
    {
        if (err != UcError.Ok)
        {
            string msg = ErrorString(err);
            throw new UnicornEngineException(err, msg);
        }
    }

    private void AssertEngine()
    {
        if (_engine == 0)
        {
            ThrowOnError(UcError.Handle);
        }
    }

    #endregion
}