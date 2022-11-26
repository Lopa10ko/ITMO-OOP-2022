namespace Banks.Tools;

public class CommissionDayException : BanksException
{
    private CommissionDayException(string errorMessage)
        : base(errorMessage) { }

    public static CommissionDayException InvalidCommissionDay(int commissionDay, DateOnly currentDate)
        => new CommissionDayException($"Invalid commission day: {commissionDay} should be less than {DateTime.DaysInMonth(currentDate.Year, currentDate.Month)}");
}