using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
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
}