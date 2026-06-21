using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
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
    
    #region Helpers
    private static uint CtlWrite(UcControlType type, uint count) => (uint)type | (count << 26) | (1u << 30);

    private static uint CtlRead(UcControlType type, uint count) => (uint)type | (count << 26) | (2u << 30);
    #endregion
}