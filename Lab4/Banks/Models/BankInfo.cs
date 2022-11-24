using Banks.Entities;

namespace Banks.Models;

public class BankInfo
{
    public BankInfo(int commissionDay, long freezeTime, decimal debitPercent, decimal untrustedUserWithdrawLimit, decimal creditLimit, decimal creditCommission, DepositRules depositRules)
    {
        CommissionDay = commissionDay;
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

    private static int ValidateCommissionPeriod(int value)
    {
        if (value > 0)
            return value;
        throw new Exception();
    }
}