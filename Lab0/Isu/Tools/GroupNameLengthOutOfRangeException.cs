namespace Isu.Tools;

public class GroupNameLengthOutOfRangeException : ArgumentOutOfRangeException
{
    public GroupNameLengthOutOfRangeException()
        : this("GroupNumber string lenght is out of range[5,7]") { }
    public GroupNameLengthOutOfRangeException(string errorMessage)
        : base(errorMessage) { }
}