using UnicornEngine.Const;

namespace UnicornEngine;
    
delegate void CodeHook(nint engine, nint address, uint size, nint userData);
delegate void BlockHook(nint engine, nint address, uint size, nint userData);
delegate void InterruptHook(nint engine, uint intNo, nint userData);
delegate bool InvalidInstructionHook(nint engine, nint userData);
delegate void InHook(nint engine, uint port, uint size, nint userData);
delegate void OutHook(nint engine, uint port, uint size, uint value, nint userData);
delegate bool TlbHook(nint engine, nint vAddress, UcMemoryType memType, ref TlbEntry result, nint userData);
delegate void EdgeGenHook(nint engine, ref TranslationBlock currentTranslationBlock, ref TranslationBlock prevTranslationBlock, nint userData);
delegate void TcgOp2Hook(nint engine, nint address, ulong arg1, ulong arg2, uint size, nint userData);
delegate ulong MemReadHook(nint engine, ulong offset, uint size, nint userData);
delegate void MemWriteHook(nint engine, ulong offset, uint size, ulong value, nint userData);
delegate void MemHook(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData);
delegate void InvalidMemHook(nint engine, UcMemoryType memType, nint address, uint size, ulong value, nint userData);