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
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsuNumber == other.IsuNumber;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Student)obj);
    }

    public override int GetHashCode()
    {
        return IsuNumber;
    }
}