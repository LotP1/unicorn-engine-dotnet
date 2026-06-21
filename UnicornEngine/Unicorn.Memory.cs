using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
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
}