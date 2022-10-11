using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class OgnpGroup : IEquatable<OgnpGroup>
{
    private const int GroupCapacity = 25;
    private List<Student> _students;

    public OgnpGroup(OgnpGroupName groupName)
    {
        _students = new List<Student>();
        OgnpGroupName = groupName;
    }

    public OgnpGroupName OgnpGroupName { get; }

    public IReadOnlyList<Student> GetStudents => _students;

    public bool Equals(OgnpGroup? other)
    {
        return other is not null && OgnpGroupName.Equals(other.OgnpGroupName);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OgnpGroup);
    }

    public override int GetHashCode()
        => OgnpGroupName.GetHashCode();

    internal void AddStudent(Student student)
    {
        if (_students.Count >= GroupCapacity)
            throw new Exception();

        // throw GroupException.ExceededGroupCapacityException(this, GroupCapacity);
        _students.Add(student);
    }

    internal void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
            throw new Exception();

        // throw GroupException.BelongingStudentException(student, this, "not");
        _students.Remove(student);
    }
}