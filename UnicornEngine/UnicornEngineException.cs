using UnicornEngine.Const;

namespace UnicornEngine;

public class UnicornEngineException(UcError err, string msg) : Exception(msg)
{
    public UcError Error { get; } = err;
}