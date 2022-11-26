namespace Banks.Tools;

public class ValueAmountException : BanksException
{
    private ValueAmountException(string errorMessage)
        : base(errorMessage) { }

    public static ValueAmountException InvalidValueAmount(decimal value)
        => new ValueAmountException($"Invalid Value: ({value}) should be nonnegative");
}