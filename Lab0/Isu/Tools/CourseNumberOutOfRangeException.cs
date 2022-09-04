namespace Isu.Tools;

public class CourseNumberOutOfRangeException : ArgumentOutOfRangeException
{
    public CourseNumberOutOfRangeException()
        : this("CourseNumber attribute is not valid or out of range[0,4]") { }
    public CourseNumberOutOfRangeException(string errorMessage)
        : base(errorMessage) { }
}