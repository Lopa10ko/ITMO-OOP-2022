namespace Isu.Tools;

public class CourseNumberNotParsableException : FormatException
{
    public CourseNumberNotParsableException()
        : this("CourseNumber is not valid. Try changing GroupName formatting") { }
    public CourseNumberNotParsableException(string errorMessage)
        : base(errorMessage) { }
}