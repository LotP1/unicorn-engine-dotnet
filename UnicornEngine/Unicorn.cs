using UnicornEngine.Const;

namespace UnicornEngine;

public partial class Unicorn
{
    private nint _engine;

    public UcError Open(UcArch arch, UcMode mode)
    {
        if (_engine != 0)
        {
            return UcError.Handle;
        }
        
        return Open(arch, mode, ref _engine);;
    }

    public UcError Close()
    {
        if (_engine == 0)
        {
            return UcError.Handle;
        }
        
        UcError err = Close(_engine);
        
        _engine = 0;
        
        return err;
    }
}