using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
    #region query
    public UcMode QueryMode()
    {
        AssertEngine();

        UcMode mode = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Mode, new nint(&mode)));
        }

        return mode;
    }

    public uint QueryPageSize()
    {
        AssertEngine();

        uint pageSize = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.PageSize, new nint(&pageSize)));
        }

        return pageSize;
    }

    public UcArch QueryArch()
    {
        AssertEngine();

        UcArch arch = 0;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Arch, new nint(&arch)));
        }

        return arch;
    }

    public bool QueryTimeout()
    {
        AssertEngine();

        bool timeout = false;

        unsafe
        {
            ThrowOnError(Bindings.Query(_engine, UcQueryType.Timeout, new nint(&timeout)));
        }

        return timeout;
    }
    #endregion
}