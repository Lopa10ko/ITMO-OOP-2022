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

    public GroupName(string groupName)
    {
        ValidateGroupName(groupName);
        Name = groupName;
        FacultyLetter = new FacultyLetter(groupName[FacultyLetterPosition]);
        StudyTypeNumber = new StudyTypeNumber(groupName[StudyTypePosition]);
        CourseNumber = new CourseNumber(groupName[CourseNumberPosition]);
    }

    public FacultyLetter FacultyLetter { get; }
    public StudyTypeNumber StudyTypeNumber { get; }
    public CourseNumber CourseNumber { get; }
    public string Name { get; }

    public bool Equals(GroupName? other)
    {
        return other is not null && Name.Equals(other.Name) && FacultyLetter.Equals(other.FacultyLetter);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GroupName);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, FacultyLetter);
    }

    private static void ValidateGroupName(string groupName)
    {
        if (groupName.Length is < MinimumLength or > MaximumLength)
        {
            throw IsuException.GroupNameException(groupName, "GroupName length is out of range");
        }

        string groupNumberSymbols = groupName.Substring(GroupNumberPositionStart, GroupNumberLength);
        bool isGroupNumberNumeric = int.TryParse(groupNumberSymbols, out _);
        if (!isGroupNumberNumeric)
        {
            throw IsuException.GroupNameException(groupName, $"GroupNumber is not a number: {groupNumberSymbols}");
        }
    }
}