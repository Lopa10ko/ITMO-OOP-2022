using Banks.Tools;

namespace Banks.Entities;

public class BankInfo
{
    public BankInfo(int commissionDay, long freezeTime, decimal debitPercent, decimal untrustedUserWithdrawLimit, decimal creditLimit, decimal creditCommission, DepositRules depositRules)
    {
        CommissionDay = ValidateCommissionPeriod(commissionDay);
        FreezeTime = new ValueAmount(freezeTime);
        DebitPercent = new ValueAmount(debitPercent);
        CreditLimit = new ValueAmount(creditLimit);
        UntrustedUserWithdrawLimit = new ValueAmount(untrustedUserWithdrawLimit);
        CreditCommission = new ValueAmount(creditCommission);
        DepositRules = depositRules;
    }

    public int CommissionDay { get; }
    public ValueAmount FreezeTime { get; }
    public ValueAmount DebitPercent { get; }
    public ValueAmount CreditLimit { get; }
    public ValueAmount CreditCommission { get; }
    public ValueAmount UntrustedUserWithdrawLimit { get; }
    public DepositRules DepositRules { get; }
    public override string ToString()
    {
        string logMessage = $"New BankInfo:\n" +
                            $"Commision Day: {CommissionDay}\n" +
                            $"Limit for unverified clients: {UntrustedUserWithdrawLimit}" +
                            $"\nDebit Account (Freeze Time: {FreezeTime}, Debit Percent: {DebitPercent})" +
                            $"\nCredit Account (Credit Limit: {CreditLimit}, Credit Commission: {CreditCommission})" +
                            $"\nDeposit Account Rules:\n";
        return DepositRules.Rules.Aggregate(logMessage, (current, rule) => current + $"Critical Value: <{rule.CriticalValue} - Percent: {rule.Percent}\n");
    }

    private static int ValidateCommissionPeriod(int value)
    {
        if (value > 0)
            return value;
        throw ValueAmountException.InvalidValueAmount(value);
    }
}