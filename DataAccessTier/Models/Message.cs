namespace DataAccessTier.Models;

public class Message
{
    public Message(Guid id, Employee employee, string text)
    {
        Id = id;
        Text = text;
        Employee = employee;
        State = MessageState.Unchecked;
        HandledTime = DateTime.MaxValue;
    }

#pragma warning disable CS8618
    protected Message() { }
#pragma warning restore CS8618
    public Guid Id { get; set; }
    public virtual Employee Employee { get; set; }
    public string Text { get; set; }
    public MessageState State { get; set; }
    public DateTime HandledTime { get; set; }
}