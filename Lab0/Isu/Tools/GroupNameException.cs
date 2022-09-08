namespace Isu.Tools;

public class GroupNameException : Exception
{
    public GroupNameException(string errorMessage)
        : base(errorMessage) { }
}