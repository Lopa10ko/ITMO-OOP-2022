using Isu.Tools;

namespace Isu.Models;

public class GroupName : IEquatable<GroupName>
{
    private const int FacultyLetterPosition = 0;
    private const int StudyTypePosition = 1;
    private const int CourseNumberPosition = 2;
    private const int GroupNumberLength = 2;
    private const int GroupNumberPositionStart = 3;
    private const int MinimumLength = 5;
    private const int MaximumLength = 7;
    private readonly string _groupName;

    public GroupName(string groupName)
    {
        ValidateGroupName(groupName);
        _groupName = groupName;
        FacultyLetter = new FacultyLetter(groupName[FacultyLetterPosition]);
        StudyTypeNumber = new StudyTypeNumber(int.Parse(_groupName[StudyTypePosition].ToString()));
        CourseNumber = new CourseNumber(int.Parse(_groupName[CourseNumberPosition].ToString()));
    }

    public FacultyLetter FacultyLetter { get; }
    public StudyTypeNumber StudyTypeNumber { get; }
    public CourseNumber CourseNumber { get; }

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

        char courseNumberSymbol = groupName[CourseNumberPosition];
        if (!char.IsDigit(courseNumberSymbol))
        {
            throw new GroupNameException($"CourseNumber is not a number: {courseNumberSymbol}");
        }

        char studyTypeSymbol = groupName[StudyTypePosition];
        if (!char.IsDigit(studyTypeSymbol))
        {
            throw new GroupNameException($"StudyType is not a number: {studyTypeSymbol}");
        }

        string groupNumberSymbols = groupName.Substring(GroupNumberPositionStart, GroupNumberLength);
        bool isGroupNumberNumeric = int.TryParse(groupNumberSymbols, out _);
        if (!isGroupNumberNumeric)
        {
            throw new GroupNameException($"GroupNumber is not a number: {groupNumberSymbols}");
        }
    }
}