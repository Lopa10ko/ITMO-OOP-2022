using Isu.Tools;
using Isu.Utils;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int MinCourseNumber = 1;
    private const int MaxCourseNumber = 4;
    private const int MinimumLength = 5;
    private const int MaximumLength = 7;
    private const int MinStudyType = 2;
    private const int MaxStudyType = 5;
    private const int GroupNumberLength = 2;
    private readonly string _groupName;

    public GroupName(string groupName)
    {
        ValidateGroupName(groupName);
        _groupName = groupName;
        FacultyLetter = groupName[(int)GroupNamePosition.FacultyLetterPosition];
    }

    public char FacultyLetter { get; }

    public CourseNumber GetCourseNumber()
        => new CourseNumber(Convert.ToInt32(_groupName[CourseNumberPosition]));

    public bool Equals(GroupName? other)
    {
        return other is not null && _groupName == other._groupName && FacultyLetter == other.FacultyLetter;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GroupName);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_groupName, FacultyLetter);
    }

    private void ValidateGroupName(string groupName)
    {
        if (groupName.Length is < MinimumLength or > MaximumLength)
        {
            throw new GroupNameException("GroupName length is out of range");
        }

        if (!char.IsUpper(groupName[(int)GroupNamePosition.FacultyLetterPosition]))
        {
            throw new GroupNameException("First letter in GroupName must be in [A-Z]");
        }

        char courseNumberSymbol = groupName[(int)GroupNamePosition.CourseNumberPosition];
        if (!char.IsDigit(courseNumberSymbol) || int.Parse(courseNumberSymbol.ToString()) is < MinCourseNumber
            or > MaxCourseNumber)
        {
            throw new GroupNameException("CourseNumber is not valid: out of range");
        }

        char studyTypeSymbol = groupName[(int)GroupNamePosition.StudyTypePosition];
        if (!char.IsDigit(studyTypeSymbol) || int.Parse(studyTypeSymbol.ToString()) is < MinStudyType
            or > MaxStudyType)
        {
            throw new GroupNameException("StudyType is not valid: out of range");
        }

        string groupNumberSymbols =
            groupName.Substring((int)GroupNamePosition.GroupNumberPositionStart, GroupNumberLength);
        bool isGroupNumberNumeric = int.TryParse(groupNumberSymbols, out _);
        if (!isGroupNumberNumeric)
        {
            throw new GroupNameException("GroupNumber is not numeric");
        }
    }
}