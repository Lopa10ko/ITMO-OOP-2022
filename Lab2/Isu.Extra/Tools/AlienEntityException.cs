using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class AlienEntityException : IsuExtraException
{
    private AlienEntityException(string errorMessage)
        : base(errorMessage) { }

    public static AlienEntityException AlienFacultyException(string facultyString)
        => new AlienEntityException($"Not existing Faculty {facultyString}");

    public static AlienEntityException AlienOgnpGroup(OgnpGroup ognpGroup)
        => new AlienEntityException($"Not registered OgnpGroup {ognpGroup.GroupName.Name}");
}