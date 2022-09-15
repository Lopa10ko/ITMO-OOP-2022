using Isu.Models;
using Isu.Services;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(int isuNumber, string name)
    {
        Name = name;
        IsuNumber = isuNumber;
    }

    public string Name { get; }
    public Group? Group { get; set; }
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

    internal void AddGroup(Group group)
    {
        if (group.Equals(Group))
        {
            throw new GroupException($"Student {Name} - {IsuNumber} has group already");
        }

        Group = group;
    }
}