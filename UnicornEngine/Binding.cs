#pragma warning disable SYSLIB1054
using System.Runtime.InteropServices;
using UnicornEngine.Const;

namespace UnicornEngine;

public static unsafe class Bindings
{
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_version")]
    internal static extern uint Version(ref uint major, ref uint minor);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_arch_supported")]
    internal static extern bool ArchSupported(UcArch arch);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_open")]
    internal static extern UcError Open(UcArch arch, UcMode mode, ref nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_close")]
    internal static extern UcError Close(nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_query")]
    internal static extern UcError Query(nint engine, UcQueryType type, nint result);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    internal static extern UcError CtlArg0(nint engine, uint control);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    internal static extern UcError CtlArg1(nint engine, uint control, ulong arg1);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    internal static extern UcError CtlArg2(nint engine, uint control, ulong arg1, ulong arg2);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_errno")]
    internal static extern UcError ErroNo(nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_strerror")]
    internal static extern sbyte* StrError(UcError code);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write")]
    internal static extern UcError RegWrite(nint engine, uint regId, byte* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read")]
    internal static extern UcError RegRead(nint engine, uint regId, byte* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write")]
    internal static extern UcError RegWriteCpReg(nint engine, RegArm regId, ArmCpRegister* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read")]
    internal static extern UcError RegReadCpReg(nint engine, RegArm regId, ArmCpRegister* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write_batch")]
    internal static extern UcError RegWriteBatch(nint engine, uint* regIds, byte** values, int count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read_batch")]
    internal static extern UcError RegReadBatch(nint engine, uint* regIds, byte** values, int count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_write")]
    internal static extern UcError MemWrite(nint engine, nint address, byte* bytes, ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_read")]
    internal static extern UcError MemRead(nint engine, nint address, byte* bytes, ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_read")]
    internal static extern UcError VMemRead(nint engine, nint address, UcProtection prot, byte* bytes, ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_write")]
    internal static extern UcError VMemWrite(nint engine, nint address, UcProtection prot, byte* bytes,
        ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_translate")]
    internal static extern UcError VMemTranslate(nint engine, nint address, UcProtection prot, ref nint pAddress);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_emu_start")]
    internal static extern UcError EmuStart(nint engine, nint beginAddress, nint untilAddress, ulong timeout, ulong count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_emu_stop")]
    internal static extern UcError EmuStop(nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    internal static extern UcError HookAddNoArgs(
        nint engine,
        ref nint hook,
        UcHookType callbackType,
        nint callback,
        nint userData,
        nint hookBegin,
        nint hookEnd);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    internal static extern UcError HookAdd1Arg(
        nint engine,
        ref nint hook,
        UcHookType callbackType,
        nint callback,
        nint userData,
        nint hookBegin,
        nint hookEnd,
        ulong arg0);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    internal static extern UcError HookAdd2Args(
        nint engine,
        ref nint hook,
        UcHookType callbackType,
        nint callback,
        nint userData,
        nint hookBegin,
        nint hookEnd,
        ulong arg0,
        ulong arg1);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_del")]
    internal static extern UcError HookDel(nint engine, nint hook);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_map")]
    internal static extern UcError MemMap(nint engine, nint address, ulong size, UcProtection perms);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_map_ptr")]
    internal static extern UcError MemMapPtr(nint engine, nint address, ulong size, UcProtection perms, nint ptr);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mmio_map")]
    internal static extern UcError MemIOMap(nint engine, nint address, ulong size,
        MmioReadHook.InternalDelegate readCallback, nint userDataRead,
        MmioWriteHook.InternalDelegate writeCallback, nint userDataWrite);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_unmap")]
    internal static extern UcError MemUnmap(nint engine, nint address, ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_protect")]
    internal static extern UcError MemProtect(nint engine, nint address, ulong size, UcProtection perms);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_regions")]
    internal static extern UcError MemRegions(nint engine, MemRegion** regions, ref uint count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_alloc")]
    internal static extern UcError ContextAlloc(nint engine, ContextObj* context);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_free")]
    internal static extern UcError MemFree(MemRegion* mem);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_save")]
    internal static extern UcError ContextSave(nint engine, ContextObj context);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write")]
    internal static extern UcError ContextRegWrite(ContextObj context, uint regId, byte* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read")]
    internal static extern UcError ContextRegRead(ContextObj context, uint regId, byte* value);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write_batch")]
    internal static extern UcError ContextRegWriteBatch(ContextObj context, uint* regIds, byte** values,
        int count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read_batch")]
    internal static extern UcError ContextRegReadBatch(ContextObj context, uint* regIds, byte** values,
        int count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_restore")]
    internal static extern UcError ContextRestore(nint engine, ContextObj context);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_size")]
    internal static extern ulong ContextSize(nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_free")]
    internal static extern UcError ContextFree(ContextObj context);
}
#pragma warning restore SYSLIB1054