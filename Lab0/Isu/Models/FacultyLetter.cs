using Isu.Tools;

namespace Isu.Models;

public class FacultyLetter : IEquatable<FacultyLetter>
{
    private readonly char _facultyLetter;

    public FacultyLetter(char facultyLetter)
    {
        ValidateFacultyLetter(facultyLetter);
        _facultyLetter = facultyLetter;
    }

    public bool Equals(FacultyLetter? other)
    {
        return other is not null && _facultyLetter.Equals(other._facultyLetter);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as FacultyLetter);
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