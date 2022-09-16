using Isu.Tools;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 4;
    private readonly int _courseNumber;

    public CourseNumber(char courseNumber)
    {
        ValidateCourseNumber(courseNumber);
        _courseNumber = int.Parse(courseNumber.ToString());
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

    private static void ValidateCourseNumber(char courseNumber)
    {
        if (!char.IsDigit(courseNumber))
        {
            throw GroupNameException.InvalidFormatException(courseNumber, "CourseNumber is not a number");
        }

        int courseNumberNumeric = int.Parse(courseNumber.ToString());
        if (courseNumberNumeric is < MinCourseNumber or > MaxCourseNumber)
        {
            throw GroupNameException.OutOfRangeException(courseNumberNumeric, "CourseNumber");
        }
    }
}