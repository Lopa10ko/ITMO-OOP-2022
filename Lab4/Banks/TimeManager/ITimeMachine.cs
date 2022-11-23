using Banks.Banks;

namespace Banks.TimeManager;
public class TimeMachine
{
    private static readonly Lazy<TimeMachine> _lazy = new Lazy<TimeMachine>(() => new TimeMachine(Guid.NewGuid()));
    private TimeMachine(Guid id)
    {
        Id = id;
        Banks = new List<Bank>();
    }

    public Guid Id { get; }
    public List<Bank> Banks { get; }
    public DateOnly CurrentDate
        => new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    public static TimeMachine GetInstance() => _lazy.Value;

    public void SkipDays(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SkipDay();
        }
    }

    private void SkipDay()
    {
        CurrentDate.AddDays(1);
        foreach (Bank bank in Banks)
        {
            bank.Notify(CurrentDate);
        }
    }
}