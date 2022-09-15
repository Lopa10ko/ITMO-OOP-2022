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
        CourseNumber = groupName.GetCourseNumber();
    }

    public GroupName GroupName { get; }
    public CourseNumber CourseNumber { get; }

    public IReadOnlyList<Student> GetStudents => _students;

    public bool Equals(Group? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GroupName.Equals(other.GroupName);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Group)obj);
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    internal void AddStudent(Student student)
    {
        if (_students.Count >= GroupCapacity)
        {
            throw new GroupException("Group capacity limit reached");
        }

        if (_students.Contains(student))
        {
            throw new GroupException("Student already in group");
        }

        _students.Add(student);
        student.Group = this;
    }

    internal void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            throw new GroupException($"Student {student.Name} - {student.IsuNumber} is not in this group to delete");
        }

        _students.Remove(student);
    }
}