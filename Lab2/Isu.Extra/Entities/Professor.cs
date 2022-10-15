using System.Text.RegularExpressions;
using Isu.Extra.Tools;

namespace Isu.Extra.Entities;

public record Professor
{
    private static readonly Regex Validation = new Regex(@"^[a-zA-Z\s\-\,\.]*$", RegexOptions.Compiled);

    public Professor(string name)
    {
        ValidateName(name);
        Name = name;
    }

    public string Name { get; }

    private static void ValidateName(string name)
    {
        if (!Validation.IsMatch(name))
            throw ProfessorException.InvalidNameFormatting(name);
    }
}