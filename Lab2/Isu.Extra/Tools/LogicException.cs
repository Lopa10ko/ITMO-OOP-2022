using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Tools;

public class LogicException : IsuExtraException
{
    private LogicException(string errorMessage)
        : base(errorMessage) { }

    public static LogicException InvalidOgnpAddition(OgnpGroup ognpGroup, Student student)
        => new LogicException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.OgnpGroupName} failed: same faculty {student.Group.GroupName.FacultyLetter}");

    public static LogicException InvalidRegisteredStudentState(OgnpGroup ognpGroup, Student student)
        => new LogicException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.OgnpGroupName} failed: already registered");

    public static LogicException OverlappingSchedules(OgnpGroup ognpGroup, Student student)
        => new LogicException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.OgnpGroupName} failed: overlapping schedules");

    public static LogicException InvalidSchedulesState(OgnpGroup ognpGroup, Student student, string groupType)
        => new LogicException($"Student {student.Name} - {student.IsuNumber} addition to OgnpGroup {ognpGroup.OgnpGroupName} failed: {groupType} schedule does not exist");
}