using Banks.Entities;
using Banks.Transactions;

namespace Banks.Accounts;

public class CreditAccount : IAccount
{
    private readonly List<ITransaction> _transactions;
    private CreditValueAmount _moneyBank;
    private ValueAmount _commission;
    private BankInfo _bankInfo;
    private bool _isVerified;
    public CreditAccount(CreditValueAmount balance, BankInfo bankInfo, bool verifiedStatus)
    {
        Id = Guid.NewGuid();
        _transactions = new List<ITransaction>();
        _moneyBank = balance;
        _bankInfo = bankInfo;
        _commission = new ValueAmount();
        _isVerified = verifiedStatus;
    }

    public Guid Id { get; }

    public void IncreaseMoneyValue(ValueAmount value)
    {
        _moneyBank.Value += value.Value;
    }

    public void DecreaseMoneyValue(ValueAmount value)
    {
        _moneyBank.Value -= ValidateValue(value.Value);
    }

    public void RemitTo(IAccount account, ValueAmount value)
    {
        DecreaseMoneyValue(value);
        account.IncreaseMoneyValue(value);
    }

    public int GetCommissionDay()
        => _bankInfo.CommissionDay;

    public void RemoveWithdrawLimit()
    {
        _isVerified = true;
    }

    public void EvaluateCommission()
    {
        _commission.Value += ValidateCreditValue();
    }

    public void AccrueCommission()
    {
        DecreaseMoneyValue(_commission);
        _commission.Value = 0;
    }

    public void AddTransaction(ITransaction transaction)
    {
        _transactions.Add(transaction);
    }

    public IReadOnlyList<ITransaction> GetTransactions()
        => _transactions.AsReadOnly();

    private decimal ValidateValue(decimal value)
        => !_isVerified ? Math.Min(_bankInfo.UntrustedUserWithdrawLimit.Value, value) : value;
    private decimal ValidateCreditValue()
        => (_moneyBank.Value < 0) ? _bankInfo.CreditCommission.Value : 0;
}