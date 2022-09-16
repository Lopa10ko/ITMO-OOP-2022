using Isu.Entities;
using Isu.Models;

namespace Isu.Tools;

public class IsuException : Exception
{
    private IsuException(string errorMessage)
        : base(errorMessage) { }

    public static IsuException ExceededGroupCapacityException(Group group, int groupCapacity)
        => new IsuException($"Unsuccessful addition to group {group.GroupName.Name}: reached capacity limit {groupCapacity}");

    public static IsuException BelongingStudentException(Student student, Group group, string belongs)
        => new IsuException($"Student {student.Name} - {student.IsuNumber} is {belongs} in the group {group.GroupName.Name}");

    public static IsuException GroupNameException(string groupName, string info = "")
        => new IsuException($"GroupName {groupName} is invalid: {info}");

    public static IsuException OutOfRangeException(int value, string type)
        => new IsuException($"{type} {value} is invalid: out of range");

    public static IsuException FacultyLetterCaseException(char facultyLetter)
        => new IsuException($"FacultyLetter {facultyLetter} is invalid: not upper case");

    public static IsuException UnknownObjectException(Group group)
        => new IsuException($"Unsuccessful operation: group {group.GroupName.Name} is uninitialized");

    public static IsuException CreatedGroupException(Group group)
        => new IsuException($"Unsuccessful operation: group {group.GroupName.Name} is already initialized");

    public static IsuException UnknownObjectException(Student student)
        => new IsuException($"Unsuccessful operation: student {student.Name} - {student.IsuNumber} is uninitialized");

    public static IsuException ChangeGroupException(Student student)
        => new IsuException($"Unsuccessful operation: student {student.Name} - {student.IsuNumber} must have oldGroup to change groups");
}
