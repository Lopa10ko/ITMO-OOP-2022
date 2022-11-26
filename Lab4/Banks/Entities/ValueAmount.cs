using Banks.Tools;

namespace Banks.Entities;

public interface IValueAmount
{
    public decimal Value { get; set; }
}

public record ValueAmount : IValueAmount
{
    private decimal _value;
    public ValueAmount(decimal value = 0)
    {
        Value = value;
    }

    public decimal Value
    {
        get => _value;
        set => _value = ValidateValue(value);
    }

    private static decimal ValidateValue(decimal value)
    {
        if (value < 0)
            throw ValueAmountException.InvalidValueAmount(value);
        return value;
    }
}

public record CreditValueAmount : IValueAmount
{
    private decimal _value;
    public CreditValueAmount(decimal value = 0)
    {
        Value = value;
    }

    public decimal Value
    {
        get => _value;
        set => _value = value;
    }
}