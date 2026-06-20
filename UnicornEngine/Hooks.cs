using UnicornEngine.Const;

namespace UnicornEngine;

public class Hook
{
    internal nint HookHandle;
    public byte[] UserData;
}

public class CodeHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;

    public CodeHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, nint address, uint size, nint userData)
    {
        Callback?.Invoke(engine, address, size, UserData);
    }

    public delegate void CallbackDelegate(nint engine, nint address, uint size, byte[] userData);
    internal delegate void InternalDelegate(nint engine, nint address, uint size, nint userData);
}
public class InterruptHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public InterruptHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, uint intNo, nint userData)
    {
        Callback?.Invoke(engine, intNo, UserData);
    }

    public delegate void CallbackDelegate(nint engine, uint intNo, byte[] userData);
    internal delegate void InternalDelegate(nint engine, uint intNo, nint userData);
}
public class InvalidInstructionHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public InvalidInstructionHook()
    {
        InternalCallback = Cb;
    }
    
    private bool Cb(nint engine, nint userData)
    {
        return Callback.Invoke(engine, UserData);
    }

    public delegate bool CallbackDelegate(nint engine, byte[] userData);
    internal delegate bool InternalDelegate(nint engine, nint userData);
}
public class InHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public InHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, uint port, uint size, nint userData)
    {
        Callback?.Invoke(engine, port, size, UserData);
    }

    public delegate void CallbackDelegate(nint engine, uint port, uint size, byte[] userData);
    internal delegate void InternalDelegate(nint engine, uint port, uint size, nint userData);
}
public class OutHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public OutHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, uint port, uint size, uint value, nint userData)
    {
        Callback?.Invoke(engine, port, size, size, UserData);
    }

    public delegate void CallbackDelegate(nint engine, uint port, uint size, uint value, byte[] userData);
    internal delegate void InternalDelegate(nint engine, uint port, uint size, uint value, nint userData);
}
public class SyscallHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public SyscallHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, nint userData)
    {
        Callback?.Invoke(engine, UserData);
    }

    public delegate void CallbackDelegate(nint engine, byte[] userData);
    internal delegate void InternalDelegate(nint engine, nint userData);
}
public class CpuIdHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public CpuIdHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, nint userData)
    {
        Callback?.Invoke(engine, UserData);
    }

    public delegate void CallbackDelegate(nint engine, byte[] userData);
    internal delegate void InternalDelegate(nint engine, nint userData);
}
public class TlbHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public TlbHook()
    {
        InternalCallback = Cb;
    }
    
    private bool Cb(nint engine, nint vAddress, UcMemoryType memType, ref TlbEntry result, nint userData)
    {
        return Callback.Invoke(engine, vAddress, memType, ref result, UserData);
    }

    public delegate bool CallbackDelegate(nint engine, nint vAddress, UcMemoryType memType, ref TlbEntry result, byte[] userData);
    internal delegate bool InternalDelegate(nint engine, nint vAddress, UcMemoryType memType, ref TlbEntry result, nint userData);
}
public class EdgeGenHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public EdgeGenHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, ref TranslationBlock currentTranslationBlock, ref TranslationBlock prevTranslationBlock, nint userData)
    {
        Callback?.Invoke(engine, ref currentTranslationBlock, ref prevTranslationBlock, UserData);
    }

    public delegate void CallbackDelegate(nint engine, ref TranslationBlock currentTranslationBlock, ref TranslationBlock prevTranslationBlock, byte[] userData);
    internal delegate void InternalDelegate(nint engine, ref TranslationBlock currentTranslationBlock, ref TranslationBlock prevTranslationBlock, nint userData);
}
public class TcgOp2Hook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public TcgOp2Hook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, nint address, ulong arg1, ulong arg2, uint size, nint userData)
    {
        Callback?.Invoke(engine, address, arg1, arg2, size, UserData);
    }

    public delegate void CallbackDelegate(nint engine, nint address, ulong arg1, ulong arg2, uint size, byte[] userData);
    internal delegate void InternalDelegate(nint engine, nint address, ulong arg1, ulong arg2, uint size, nint userData);
}
public class MmioReadHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public MmioReadHook()
    {
        InternalCallback = Cb;
    }
    
    private ulong Cb(nint engine, ulong offset, uint size, nint userData)
    {
        return Callback.Invoke(engine, offset, size, UserData);
    }

    public delegate ulong CallbackDelegate(nint engine, ulong offset, uint size, byte[] userData);
    internal delegate ulong InternalDelegate(nint engine, ulong offset, uint size, nint userData);
}

public class MmioWriteHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public MmioWriteHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, ulong offset, uint size, ulong value, nint userData)
    {
        Callback?.Invoke(engine, offset, size, value, UserData);
    }

    public delegate void CallbackDelegate(nint engine, ulong offset, uint size, ulong value, byte[] userData);
    internal delegate void InternalDelegate(nint engine, ulong offset, uint size, ulong value, nint userData);
}
public class MemHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public MemHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData)
    {
        Callback?.Invoke(engine, memType, address, size, value, UserData);
    }

    public delegate void CallbackDelegate(nint engine, UcMemoryType memType, nint address, uint size, ulong value, byte[] userData);
    internal delegate void InternalDelegate(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData);
}
public class InvalidMemHook : Hook
{
    public CallbackDelegate Callback;
    internal readonly InternalDelegate InternalCallback;
    
    public InvalidMemHook()
    {
        InternalCallback = Cb;
    }
    
    private void Cb(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData)
    {
        Callback?.Invoke(engine, memType, address, size, value, UserData);
    }

    public delegate void CallbackDelegate(nint engine, UcMemoryType memType, nint address, uint size, ulong value, byte[] userData);
    internal delegate void InternalDelegate(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData);
}