using Isu.Tools;

namespace Isu.Models;

public class StudyTypeNumber : IEquatable<StudyTypeNumber>
{
    private const int MinStudyType = 2;
    private const int MaxStudyType = 5;
    private readonly int _studyTypeNumber;

    public StudyTypeNumber(char studyTypeNumber)
    {
        ValidateStudyType(studyTypeNumber);
        _studyTypeNumber = int.Parse(studyTypeNumber.ToString());
    }

    public bool Equals(StudyTypeNumber? other)
    {
        return other is not null && _studyTypeNumber == other._studyTypeNumber;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as StudyTypeNumber);
    }

    public override int GetHashCode()
        => _studyTypeNumber;

    private static void ValidateStudyType(char studyTypeNumber)
    {
        if (!char.IsDigit(studyTypeNumber))
        {
            throw IsuException.GroupNameException($"StudyType is not a number: {studyTypeNumber}");
        }

        int studyTypeNumberNumeric = int.Parse(studyTypeNumber.ToString());
        if (studyTypeNumberNumeric is < MinStudyType or > MaxStudyType)
        {
            throw IsuException.OutOfRangeException(studyTypeNumberNumeric, "StudyType");
        }
    }
}