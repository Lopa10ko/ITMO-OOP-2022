using Isu.Tools;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 4;
    private readonly int _courseNumber;

    public CourseNumber(int courseNumber)
    {
        ValidateCourseNumber(courseNumber);
        _courseNumber = courseNumber;
    }

    public bool Equals(CourseNumber? other)
    {
        return other is not null && _courseNumber == other._courseNumber;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CourseNumber);
    }

    public override int GetHashCode()
        => _courseNumber;

    private void ValidateCourseNumber(int courseNumber)
    {
        if (courseNumber is < MinCourseNumber or > MaxCourseNumber)
        {
            throw IsuException.OutOfRangeException(courseNumber, "CourseNumber");
        }
    }
}