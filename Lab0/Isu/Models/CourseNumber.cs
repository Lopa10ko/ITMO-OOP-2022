using Isu.Tools;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    private const int MinNumber = 1;
    private const int MaxNumber = 4;
    private readonly int _courseNumber;

    public CourseNumber(int courseNumber)
    {
        _courseNumber = courseNumber;
    }

    public bool Equals(CourseNumber? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _courseNumber == other._courseNumber;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CourseNumber)obj);
    }

    public override int GetHashCode()
    {
        return _courseNumber;
    }
}