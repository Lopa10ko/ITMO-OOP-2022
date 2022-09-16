using Isu.Models;
using Isu.Tools;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int GroupCapacity = 25;
    private List<Student> _students;

    public Group(GroupName groupName)
    {
        _students = new List<Student>();
        GroupName = groupName;
    }

    public GroupName GroupName { get; }

    public IReadOnlyList<Student> GetStudents => _students;

    public bool Equals(Group? other)
    {
        return other is not null && GroupName.Equals(other.GroupName);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Group);
    }

    public override int GetHashCode()
        => GroupName.GetHashCode();

    internal void AddStudent(Student student)
    {
        if (_students.Count >= GroupCapacity)
        {
            throw IsuException.ExceededGroupCapacityException(this, GroupCapacity);
        }

        if (_students.Contains(student))
        {
            throw IsuException.BelongingStudentException(student, this, "already");
        }

        _students.Add(student);
    }

    internal void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            throw IsuException.BelongingStudentException(student, this, "not");
        }

        _students.Remove(student);
    }
}