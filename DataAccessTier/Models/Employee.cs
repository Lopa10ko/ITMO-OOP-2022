namespace DataAccessTier.Models;

public class Employee
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
}