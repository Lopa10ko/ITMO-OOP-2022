using Isu.Entities;
using Isu.Extra.Models;
using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Entities;

public class OgnpGroup : IEquatable<OgnpGroup>
{
    private const int GroupCapacity = 25;
    private readonly List<Student> _students;

    public OgnpGroup(OgnpGroupName groupName)
    {
        _students = new List<Student>();
        GroupName = groupName;
    }

    public OgnpGroupName GroupName { get; }

    public IReadOnlyList<Student> GetStudents => _students;

    public bool Equals(OgnpGroup? other)
    {
        return other is not null && GroupName.Equals(other.GroupName);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as OgnpGroup);
    }

    public override int GetHashCode()
        => GroupName.GetHashCode();

    internal void AddStudent(Student student)
    {
        if (_students.Count >= GroupCapacity)
            throw OgnpGroupException.ExceededCapacityLimit(this, student, GroupCapacity);
        if (_students.Contains(student))
            throw OgnpGroupException.PointlessStudentAddition(this, student);

        _students.Add(student);
    }

    internal void RemoveStudent(Student student)
    {
        if (!_students.Contains(student))
            throw OgnpGroupException.PointlessStudentRemoval(this, student);

        _students.Remove(student);
    }
}