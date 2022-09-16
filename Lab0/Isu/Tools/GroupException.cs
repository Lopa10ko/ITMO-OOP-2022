using Isu.Entities;

namespace Isu.Tools;

public class GroupException : IsuExceptions
{
    private GroupException(string errorMessage)
        : base(errorMessage) { }

    public static GroupException ExceededGroupCapacityException(Group group, int groupCapacity)
        => new GroupException($"Unsuccessful addition to group {group.GroupName.Name}: reached capacity limit {groupCapacity}");

    public static GroupException BelongingStudentException(Student student, Group group, string belongs)
        => new GroupException($"Student {student.Name} - {student.IsuNumber} is {belongs} in the group {group.GroupName.Name}");

    public static GroupException PointlessChangeException(Student student, Group group)
        => new GroupException($"Pointless change: student {student.Name} - {student.IsuNumber} is in the group {group.GroupName.Name} already");

    public static GroupException StudentInOtherGroupException(Student student, Group group)
        => new GroupException($"Addition to group {group.GroupName.Name} failed: student {student.Name} - {student.IsuNumber} is in the group {student.Group.GroupName.Name}");
}