using System.Runtime.InteropServices;
using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
    private nint _engine;

    private readonly List<Hook> _hooks = new();

    public static void Version(out uint major, out uint minor, out uint patch, out uint extra)
    {
        major = 0;
        minor = 0;

        uint version = Bindings.Version(ref major, ref minor);

        extra = version & 0xff;
        patch = (version >> 8) & 0xff;
    }

    public static void Version(out uint major, out uint minor, out uint patch)
    {
        major = 0;
        minor = 0;

        uint version = Bindings.Version(ref major, ref minor);

        patch = (version >> 8) & 0xff;
    }

    public static bool ArchSupported(UcArch arch)
    {
        return Bindings.ArchSupported(arch);
    }

    public void Open(UcArch arch, UcMode mode)
    {
        if (_engine != 0)
        {
            ThrowOnError(UcError.Handle);
        }

        ThrowOnError(Bindings.Open(arch, mode, ref _engine));
    }

    public void Close()
    {
        AssertEngine();

        ThrowOnError(Bindings.Close(_engine));

        _engine = 0;
    }
    
    public UcError LastError()
    {
        AssertEngine();

        return Bindings.ErroNo(_engine);
    }

    public static string ErrorString(UcError err)
    {
        unsafe
        {
            return new string(Bindings.StrError(err));
        }
    }

    public void StartEmulation(nint beginAddress, nint endAddress, ulong timeout, ulong count)
    {
        AssertEngine();

        ThrowOnError(Bindings.EmuStart(_engine, beginAddress, endAddress, timeout, count));
    }

    public void StopEmulation()
    {
        AssertEngine();

        ThrowOnError(Bindings.EmuStop(_engine));
    }

    #region Helpers
    private static void ThrowOnError(UcError err)
    {
        if (err != UcError.Ok)
        {
            string msg = ErrorString(err);
            throw new UnicornEngineException(err, msg);
        }
    }

    private void AssertEngine()
    {
        if (_engine == 0)
        {
            ThrowOnError(UcError.Handle);
        }
    }

    #endregion
}