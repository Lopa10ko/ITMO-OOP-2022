using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class OgnpGroupException : IsuExtraException
{
    private OgnpGroupException(string errorMessage)
        : base(errorMessage) { }

    public static OgnpGroupException ExceededCapacityLimit(OgnpGroup ognpGroup, Student student, int capacityLimit)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.OgnpGroupName} failed: exceeded CapacityLimit of {capacityLimit}");

    public static OgnpGroupException PointlessStudentAddition(OgnpGroup ognpGroup, Student student)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} is already in OgnpGroup {ognpGroup.OgnpGroupName}");

    public static OgnpGroupException PointlessStudentRemoval(OgnpGroup ognpGroup, Student student)
        => new OgnpGroupException($"Student {student.Name} - {student.IsuNumber} not in OgnpGroup {ognpGroup.OgnpGroupName} to remove");

    public static OgnpGroupException GroupAlreadyExists(OgnpGroup ognpGroup)
        => new OgnpGroupException($"OgnpGroup {ognpGroup.OgnpGroupName} is already registered");
}