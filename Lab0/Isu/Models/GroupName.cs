using Isu.Tools;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int FacultyLetterPosition = 0;
    private const int StudyTypePosition = 1;
    private const int CourseNumberPosition = 2;
    private const int GroupNumberPositionStart = 3;
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
        FacultyLetter = groupName[FacultyLetterPosition];
    }

    public char FacultyLetter { get; }

    public CourseNumber GetCourseNumber()
        => new CourseNumber(int.Parse(_groupName[CourseNumberPosition].ToString()));

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

        if (!char.IsUpper(groupName[FacultyLetterPosition]))
        {
            throw new GroupNameException("First letter in GroupName must be in [A-Z]");
        }

        char studyTypeSymbol = groupName[StudyTypePosition];
        if (!char.IsDigit(studyTypeSymbol) || int.Parse(studyTypeSymbol.ToString()) is < MinStudyType
                or > MaxStudyType)
        {
            throw new GroupNameException("StudyType is not valid: out of range");
        }

        string groupNumberSymbols = groupName.Substring(GroupNumberPositionStart, GroupNumberLength);
        bool isGroupNumberNumeric = int.TryParse(groupNumberSymbols, out _);
        if (!isGroupNumberNumeric)
        {
            throw new GroupNameException("GroupNumber is not numeric");
        }
    }
}