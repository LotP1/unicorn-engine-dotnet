# Unicorn Engine for .NET
.NET binding/wrapper for the [Unicorn engine](https://github.com/unicorn-engine/unicorn).

## Notes
Due to the increased stack security included in .NET 9 it is necessary to add the
`<CETCompat>false</CETCompat>`
linker option to your proj file in .NET 9+ projects.

### Examples

This is a sample of how to run the emulator in C# using unicorn-engine-dotnet.
```csharp
ulong addr = 0x10000;

Unicorn uc = new Unicorn();
uc.Open(UcArch.Arm64, UcMode.LittleEndian);
        
uc.MemMap((nint)addr, 2 * 1024 * 1024, UcProtection.All);
uc.MemWrite((nint)addr, [0x00, 0x00, 0x88, 0xD2]);

uc.StartEmulation((nint)addr, (nint)addr + 4, 0, 1);
 
Span<ulong> x0 = [0];
        
uc.RegRead(RegArm64.X0, MemoryMarshal.AsBytes(x0));
        
Console.WriteLine($"X0: {x0[0]:X}");
```

#### Registers
Reading and writing to registers.

```csharp
// Assume emulator was opened as UcArch.Arm64.

Span<ulong> val = [0];

// Reading from registers.
uc.RegRead(RegArm64.X3, MemoryMarshal.AsBytes(val));
// Writing to registers.
uc.RegWrite(RegArm64.X2, MemoryMarshal.AsBytes(val));
```

#### Memory
Mapping, unmapping, reading, writing and changing memory permissions.
```csharp
nint addr = 0x100000;
// Getting memory regions.
uc.MemRegions(out Span<MemRegion> regions);
// Remember to dispose of the region objects afterwards
for (int i = 0; i < regions.Length; i++)
{
    uc.MemFree(ref regions[i]);
}

// Getting memory page size.
uc.GetPageSize(out uint pageSize)

// Mapping memory.
uc.MemMap(addr, 8 * 1024, UcProtection.All);
// Unmapping memory.
uc.MemUnmap(addr + (4 * 1024), 4 * 1024);
// Changing memory permissions.
uc.MemProtect(addr, 4 * 1024, UcProtection.Read | UcProtection.Exec);

// Code to write to memory.
Span<byte> code = 
[
    0x01,
    0x00,
    0x88,
    0xD2
]

// Writing to memory.
uc.MemWrite(addr, code);
// Reading to memory (reading 8 bytes, so we get the 4 code bytes + 4 empty bytes).
uc.MemRead(addr, 8, out Span<byte> buffer);
```

#### Hooking
Adding and removing hooks. 
```csharp
// Adding a memory read hook.
uc.HookMemRead(addr, addr + 4 * 1024, (type, address, size, value, userData) =>
    {
        Console.WriteLine($"stuff was read from memory. userdata: {Encoding.ASCII.GetString(userData)}");
    }, "dummy data"u8.ToArray(), out MemHook hook);
// Deleting a hook.
uc.HookDelete(hook);
```

#### Contexts
Capturing and restoring contexts.
```csharp
// ContextAllocate will create a new Context object
// which you have to manually dispose afterwards using ContextFree.

// Capturing the context.
uc.ContextAllocate(out ContextObj context);

// Modify context...

// Restore emulator to context state.
uc.ContextRestore(ref context);

// Dispose context.
uc.ContextFree(ref context);
```

### Licensing
unicorn-engine-dotnet is licensed under LGPL-2.1 [LGPL-2.1 License](/LICENSE).