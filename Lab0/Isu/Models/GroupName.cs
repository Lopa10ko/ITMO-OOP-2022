using System.ComponentModel.DataAnnotations;
using Isu.Tools;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int FacultyLetterPosition = 0;
    private const int CourseNumberPosition = 2;
    /*[Required]
    [StringLength(7, MinimumLength = 5)]*/
    private readonly string _groupName;

    public GroupName(string groupName)
    {
        if (groupName.Length is < 5 or > 7)
        {
            throw new GroupNameLengthOutOfRangeException();
        }

        _groupName = groupName;
        FacultyLetter = groupName[FacultyLetterPosition];
    }

    public char FacultyLetter { get; }
    public int GetCourseNumber()
        => Convert.ToInt32(_groupName[CourseNumberPosition]);

    public bool Equals(GroupName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _groupName == other._groupName && FacultyLetter == other.FacultyLetter;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GroupName)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_groupName, FacultyLetter);
    }
}