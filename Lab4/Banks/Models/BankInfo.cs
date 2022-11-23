using Banks.Entities;

namespace Banks.Models;

public class BankInfo
{
    public BankInfo(int commissionPeriod, long freezeTime, decimal debitPercent, decimal untrustedUserWithdrawLimit, decimal creditLimit, decimal creditCommission, DepositRules depositRules)
    {
        CommissionPeriod = commissionPeriod;
        FreezeTime = freezeTime;
        DebitPercent = debitPercent;
        CreditLimit = creditLimit;
        UntrustedUserWithdrawLimit = untrustedUserWithdrawLimit;
        CreditCommission = creditCommission;
        DepositRules = depositRules;
    }

    public int CommissionPeriod { get; }
    public long FreezeTime { get; }
    public decimal DebitPercent { get; }
    public decimal CreditLimit { get; }
    public decimal CreditCommission { get; }
    public decimal UntrustedUserWithdrawLimit { get; }
    public DepositRules DepositRules { get; }
}