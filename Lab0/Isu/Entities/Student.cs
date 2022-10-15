using Isu.Models;
using Isu.Services;
using Isu.Tools;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    internal Student(int isuNumber, string name, Group group)
    {
        Name = name;
        IsuNumber = isuNumber;
        Group = group;
        group.AddStudent(this);
    }

    public string Name { get; }
    public Group Group { get; private set; }
    public int IsuNumber { get; }

    public bool Equals(Student? other)
    {
        return other is not null && other.IsuNumber == IsuNumber;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Student);
    }

    public override int GetHashCode()
        => IsuNumber;

    internal void ChangeGroup(Group group)
    {
        if (group.Equals(Group))
        {
            throw GroupException.PointlessChangeException(this, group);
        }

        group.AddStudent(this);
        Group.RemoveStudent(this);
        Group = group;
    }
}