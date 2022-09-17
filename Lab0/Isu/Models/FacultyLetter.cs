using Isu.Tools;

namespace Isu.Models;

public record FacultyLetter
{
    private readonly char _facultyLetter;

    public FacultyLetter(char facultyLetter)
    {
        ValidateFacultyLetter(facultyLetter);
        _facultyLetter = facultyLetter;
    }

    public override int GetHashCode()
        => _facultyLetter.GetHashCode();

    private static void ValidateFacultyLetter(char facultyLetter)
    {
        if (!char.IsUpper(facultyLetter))
        {
            throw GroupNameException.InvalidFormatException(facultyLetter, "FacultyLetter is not uppercase");
        }
    }
}