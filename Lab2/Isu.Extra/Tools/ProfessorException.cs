namespace Isu.Extra.Tools;

public class ProfessorException : IsuExtraException
{
    private ProfessorException(string errorMessage)
        : base(errorMessage) { }

    public static ProfessorException InvalidNameFormatting(string name)
        => new ProfessorException($"Invalid formatting in ProfessorName {name}");
}