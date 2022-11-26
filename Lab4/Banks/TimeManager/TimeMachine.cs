using Banks.Banks;

namespace Banks.TimeManager;

public class TimeMachine
{
    private static readonly Lazy<TimeMachine> _lazy = new (() => new TimeMachine(Guid.NewGuid()));
    private readonly List<Bank> _banks;
    private DateOnly _dateOnly;
    private TimeMachine(Guid id)
    {
        Id = id;
        _banks = new List<Bank>();
        _dateOnly = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }

    public Guid Id { get; }
    public DateOnly CurrentDate
    {
        get => _dateOnly;
        private set => _dateOnly = value;
    }

    public static TimeMachine GetInstance() => _lazy.Value;

    public void SkipDays(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SkipDay();
        }
    }

    internal void AddObserverBank(Bank bank)
    {
        _banks.Add(bank);
    }

    private void SkipDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        foreach (Bank bank in _banks)
        {
            bank.Notify(CurrentDate);
        }
    }
}