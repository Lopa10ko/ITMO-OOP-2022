namespace DataAccessTier.Models;

public class Report
{
    public Report(Guid id, DateTime formedDate, Employee employee, string fullText)
    {
        Id = id;
        FormedDate = formedDate;
        Employee = employee;
        FullText = fullText;
    }

#pragma warning disable CS8618
    protected Report() { }
#pragma warning restore CS8618
    public Guid Id { get; set; }
    public DateTime FormedDate { get; set; }
    public virtual Employee Employee { get; set; }
    public string FullText { get; set; }

    public override string ToString()
        => FullText;
}