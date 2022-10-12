using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class OgnpGroupException : IsuExtraException
{
    private OgnpGroupException(string errorMessage)
        : base(errorMessage) { }

    public static OgnpGroupException ExceededCapacityLimit(OgnpGroup ognpGroup, Student student, int capacityLimit)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.GroupName} failed: exceeded CapacityLimit of {capacityLimit}");

    public static OgnpGroupException PointlessStudentAddition(OgnpGroup ognpGroup, Student student)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} is already in OgnpGroup {ognpGroup.GroupName}");

    public static OgnpGroupException PointlessStudentRemoval(OgnpGroup ognpGroup, Student student)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} not in OgnpGroup {ognpGroup.GroupName} to remove");

    public static OgnpGroupException GroupAlreadyExists(OgnpGroup ognpGroup)
        => new OgnpGroupException($"OgnpGroup {ognpGroup.GroupName.Name} is already registered");
}