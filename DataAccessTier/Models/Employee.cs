namespace DataAccessTier.Models;

public class Employee : IEquatable<Employee>
{
    public Employee(Guid id, string name)
    {
        Id = id;
        Name = name;
        LedEmployees = new List<Employee>();
        Messages = new List<Message>();
    }

#pragma warning disable CS8618
    protected Employee() { }
#pragma warning restore CS8618
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Employee> LedEmployees { get; set; }

    // TODO: fix timing issue to form proper report
    public virtual ICollection<Message> Messages { get; set; }

    public bool Equals(Employee? other) => other is not null && Id.Equals(other.Id);
    public override bool Equals(object? obj) => Equals(obj as Employee);
    public override int GetHashCode() => Id.GetHashCode();
}