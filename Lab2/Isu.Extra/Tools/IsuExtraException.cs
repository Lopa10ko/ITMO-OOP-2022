namespace Isu.Extra.Tools;

public class IsuExtraException : Exception
{
    public IsuExtraException(string errorMessage)
        : base(errorMessage) { }
}