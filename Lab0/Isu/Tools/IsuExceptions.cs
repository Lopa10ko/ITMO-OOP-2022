namespace Isu.Tools;

public class IsuExceptions : Exception
{
    public IsuExceptions(string errorMessage)
        : base(errorMessage) { }
}