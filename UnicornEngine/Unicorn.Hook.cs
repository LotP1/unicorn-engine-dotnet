using System.Runtime.InteropServices;
using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
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
}