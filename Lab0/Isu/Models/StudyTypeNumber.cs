using Isu.Tools;

namespace Isu.Models;

public record StudyTypeNumber
{
    private const int MinStudyType = 2;
    private const int MaxStudyType = 5;
    private readonly int _studyTypeNumber;

    public StudyTypeNumber(char studyTypeNumber)
    {
        ValidateStudyType(studyTypeNumber);
        _studyTypeNumber = int.Parse(studyTypeNumber.ToString());
    }

    public override int GetHashCode()
        => _studyTypeNumber;

    private static void ValidateStudyType(char studyTypeNumber)
    {
        if (!char.IsDigit(studyTypeNumber))
        {
            throw GroupNameException.InvalidFormatException(studyTypeNumber, "StudyType is not a number");
        }

        int studyTypeNumberNumeric = int.Parse(studyTypeNumber.ToString());
        if (studyTypeNumberNumeric is < MinStudyType or > MaxStudyType)
        {
            throw GroupNameException.OutOfRangeException(studyTypeNumberNumeric, "StudyType");
        }
    }
}