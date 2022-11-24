namespace Banks.Entities;

public record DepositRule
{
    public DepositRule(decimal criticalValue, decimal percent)
    {
        CriticalValue = new ValueAmount(criticalValue);
        Percent = new ValueAmount(percent);
    }

    public ValueAmount CriticalValue { get; }
    public ValueAmount Percent { get; }

    public bool IsOverlapping(DepositRule otherRule)
        => CriticalValue.Equals(otherRule.CriticalValue);

    public DepositRule GetMaximumPercent(DepositRule otherRule)
        => Percent.Value > otherRule.Percent.Value ? this : otherRule;
}