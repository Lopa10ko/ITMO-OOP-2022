using Isu.Entities;

namespace Isu.Tools;

public class GroupLogicException : IsuExceptions
{
    private GroupLogicException(string errorMessage)
        : base(errorMessage) { }

    public static GroupLogicException UnknownObjectException(Group group)
        => new GroupLogicException($"Unsuccessful operation: group {group.GroupName.Name} is uninitialized");

    public static GroupLogicException CreatedGroupException(Group group)
        => new GroupLogicException($"Unsuccessful operation: group {group.GroupName.Name} is already initialized");

    public static GroupLogicException UnknownObjectException(Student student)
        => new GroupLogicException($"Unsuccessful operation: student {student.Name} - {student.IsuNumber} is uninitialized");

    public static GroupLogicException ChangeGroupException(Student student)
        => new GroupLogicException($"Unsuccessful operation: student {student.Name} - {student.IsuNumber} must have oldGroup to change groups");
}