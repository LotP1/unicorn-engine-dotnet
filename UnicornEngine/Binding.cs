using System.Runtime.InteropServices;
using UnicornEngine.Const;

namespace UnicornEngine;

public unsafe partial class Unicorn
{
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_version")]
    private static extern int Version(ref uint major, ref uint minor);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_arch_supported")]
    private static extern bool ArchSupported(UcArch arch);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_open")]
    private static extern UcError Open(UcArch arch, UcMode mode, ref nint engine);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_close")]
    private static extern UcError Close(nint engine);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_query")]
    private static extern UcError UcQuery(nint engine, UcQueryType type, nint result);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    private static extern UcError UcCtlArg1(nint engine, uint/*UcControl*/ control, ulong arg1);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    private static extern UcError UcCtlArg2(nint engine, uint/*UcControl*/ control, ulong arg1, ulong arg2);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    private static extern UcError UcCtlArg3(nint engine, uint/*UcControl*/ control, ulong arg1, ulong arg2, ulong arg3);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_ctl")]
    private static extern UcError UcCtlArg4(nint engine, uint/*UcControl*/ control, ulong arg1, ulong arg2, ulong arg3, ulong arg4);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_errno")]
    private static extern UcError ErroNo(nint engine);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_strerror")]
    private static extern string StrError(UcError code);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write")]
    private static extern UcError RegWrite(nint engine, uint regId, ref ulong value);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read")]
    private static extern UcError RegRead(nint engine, uint regId, ref ulong value);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write2")]
    private static extern UcError RegWrite2(nint engine, uint regId, Span<byte> value, ref ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read2")]
    private static extern UcError RegRead2(nint engine, uint regId, Span<byte> value, ref ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write_batch")]
    private static extern UcError RegWriteBatch(nint engine, Span<uint> regIds, ref Span<ulong> values, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read_batch")]
    private static extern UcError RegReadBatch(nint engine, Span<uint> regIds, ref Span<ulong> values, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_write_batch2")]
    private static extern UcError RegWriteBatch2(nint engine, Span<uint> regIds, ref Span<ulong> values, Span<ulong> sizes, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_reg_read_batch2")]
    private static extern UcError RegReadBatch2(nint engine, Span<uint> regIds, ref Span<ulong> values, Span<ulong> sizes, int count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_write")]
    private static extern UcError MemWrite(nint engine, ulong address, Span<byte> bytes, ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_read")]
    private static extern UcError MemRead(nint engine, ulong address, Span<byte> bytes, ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_read")]
    private static extern UcError VMemRead(nint engine, ulong address, UcProtection prot, Span<byte> bytes, ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_write")]
    private static extern UcError VMemWrite(nint engine, ulong address, UcProtection prot, Span<byte> bytes, ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_vmem_translate")]
    private static extern UcError VMemTranslate(nint engine, ulong address, UcProtection prot, ref ulong pAddress);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_emu_start")]
    private static extern UcError EmuStart(nint engine, ulong beginAddress, ulong untilAddress, ulong timeout, ulong count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_emu_stop")]
    private static extern UcError EmuStop(nint engine);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    private static extern UcError HookAddNoarg(
        nint engine,
        ref nint hook,
        UcHookType callbackType,
        nint callback,
        nint userData,
        ulong hookBegin,
        ulong hookEnd);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    private static extern UcError HookAddArg0(
        nint engine,
        ref nint hook,
        int callbackType,
        nint callback,
        nint userData,
        ulong hookBegin,
        ulong hookEnd,
        ulong arg0);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_add")]
    private static extern UcError HookAddArg0Arg1(
        nint engine,
        ref nint hook,
        int callbackType,
        nint callback,
        nint userData,
        ulong hookBegin,
        ulong hookEnd,
        ulong arg0,
        ulong arg1);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_hook_del")]
    private static extern UcError HookDel(nint engine, nint hook);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_map")]
    private static extern UcError MemMap(nint engine, ulong address, ulong size, UcProtection perms);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_map_ptr")]
    private static extern UcError MemMapPtr(nint engine, ulong address, ulong size, UcProtection perms, nint ptr);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mmio_map")]
    private static extern UcError MemIOMap(nint engine, ulong address, ulong size, MemReadHook readCallback, nint userDataRead, MemWriteHook writeCallback, nint userDataWrite);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_unmap")]
    private static extern UcError MemUnmap(nint engine, ulong address, ulong size);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_protect")]
    private static extern UcError MemProtect(nint engine, ulong address, ulong size, UcProtection perms);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_mem_regions")]
    private static extern UcError MemRegions(nint engine, out MemRegion* regions, ref uint count);

    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_alloc")]
    private static extern UcError ContextAlloc(nint engine, ref nint context);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_free")]
    private static extern UcError Free(in MemRegion* mem);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_save")]
    private static extern UcError ContextSave(nint engine, nint context);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write")]
    private static extern UcError ContextRegWrite(nint context, uint regId, ref ulong value);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read")]
    private static extern UcError ContextRegRead(nint context, uint regId, ref ulong value);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write2")]
    private static extern UcError ContextRegWrite2(nint context, uint regId, Span<byte> value, ref ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read2")]
    private static extern UcError ContextRegRead2(nint context, uint regId, Span<byte> value, ref ulong size);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write_batch")]
    private static extern UcError ContextRegWriteBatch(nint context, Span<uint> regIds, ref Span<ulong> values, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read_batch")]
    private static extern UcError ContextRegReadBatch(nint context, Span<uint> regIds, ref Span<ulong> values, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_write_batch2")]
    private static extern UcError ContextRegWriteBatch2(nint context, Span<uint> regIds, ref Span<ulong> values, Span<ulong> sizes, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_reg_read_batch2")]
    private static extern UcError ContextRegReadBatch2(nint context, Span<uint> regIds, ref Span<ulong> values, Span<ulong> sizes, int count);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_restore")]
    private static extern UcError ContextRestore(nint engine, nint context);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_size")]
    private static extern UcError ContextSize(nint engine);
    
    [DllImport("unicorn", CallingConvention = CallingConvention.Cdecl, EntryPoint = "uc_context_free")]
    private static extern UcError ContextFree(nint context);
}