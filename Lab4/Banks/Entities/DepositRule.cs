namespace Banks.Entities;

public record DepositRule
{
    public DepositRule(decimal criticalValue, decimal percent)
    {
        CriticalValue = ValidateValue(criticalValue);
        Percent = ValidateValue(percent);
    }

    public decimal CriticalValue { get; }
    public decimal Percent { get; }

    public bool IsOverlapping(DepositRule otherRule)
        => CriticalValue.Equals(otherRule.CriticalValue);

    public DepositRule GetMaximumPercent(DepositRule otherRule)
        => Percent > otherRule.Percent ? this : otherRule;

    private static decimal ValidateValue(decimal value)
    {
        if (value <= 0)
            throw new Exception();
        return value;
    }
}