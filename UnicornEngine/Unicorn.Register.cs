using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
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

}