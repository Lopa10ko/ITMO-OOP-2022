using System.Transactions;
using Banks.Models;

namespace Banks.Accounts;

public class DebitAccount : IAccount
{
    private readonly List<Transaction> _transactions;
    private decimal _moneyBank;
    private decimal _commission;
    private BankInfo _bankInfo;
    private bool _isVerified;
    public DebitAccount(decimal balance, BankInfo bankInfo, bool verifiedStatus)
    {
        Id = Guid.NewGuid();
        _transactions = new List<Transaction>();
        _moneyBank = balance;
        _bankInfo = bankInfo;
        _isVerified = verifiedStatus;
    }

    public Guid Id { get; }

    public void IncreaseMoneyValue(decimal value)
    {
        _moneyBank += value;
    }

    public void DecreaseMoneyValue(decimal value)
    {
        _moneyBank -= ValidateValue(value);
    }

    public void RemitTo(decimal value, IAccount account)
    {
        DecreaseMoneyValue(value);
        account.IncreaseMoneyValue(value);
    }

    public int GetCommissionPeriod()
        => _bankInfo.CommissionPeriod;

    public void RemoveWithdrawLimit()
    {
        _isVerified = true;
    }

    public void EvaluateCommission()
    {
        _commission += _moneyBank * _bankInfo.DebitPercent;
    }

    public void AccrueCommission()
    {
        IncreaseMoneyValue(_commission);
        _commission = 0;
    }

    public IReadOnlyList<Transaction> GetTransactions()
        => _transactions.AsReadOnly();

    private decimal ValidateValue(decimal value)
        => !_isVerified ? Math.Min(_bankInfo.UntrustedUserWithdrawLimit, value) : value;
}