using Banks.Entities;
using Banks.Transactions;

namespace Banks.Accounts;

public class DepositAccount : IAccount
{
    private readonly List<ITransaction> _transactions;
    private ValueAmount _moneyBank;
    private ValueAmount _commission;
    private BankInfo _bankInfo;
    private bool _isVerified;
    public DepositAccount(ValueAmount balance, BankInfo bankInfo, bool verifiedStatus)
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
        _commission.Value += _moneyBank.Value * GetDepositPercent().Value;
    }

    public void AccrueCommission()
    {
        IncreaseMoneyValue(_commission);
        _commission.Value = 0;
    }

    public void AddTransaction(ITransaction transaction)
    {
        _transactions.Add(transaction);
    }

    public IReadOnlyList<ITransaction> GetTransactions()
        => _transactions.AsReadOnly();
    public override string ToString()
        => $"{_moneyBank.Value}";

    private decimal ValidateValue(decimal value)
        => !_isVerified ? Math.Min(_bankInfo.UntrustedUserWithdrawLimit.Value, value) : value;

    private ValueAmount GetDepositPercent()
    {
        var percent = new ValueAmount(_bankInfo.DepositRules.MaximumPercent.Value);
        foreach (DepositRule depositRule in _bankInfo.DepositRules.Rules)
        {
            if (_moneyBank.Value <= depositRule.CriticalValue.Value)
                percent = depositRule.Percent;
        }

        return percent;
    }
}