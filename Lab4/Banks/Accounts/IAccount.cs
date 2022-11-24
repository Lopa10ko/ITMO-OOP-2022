using Banks.Entities;
using Transaction = System.Transactions.Transaction;

namespace Banks.Accounts;

public interface IAccount
{
    public Guid Id { get; }
    void IncreaseMoneyValue(ValueAmount value);
    void DecreaseMoneyValue(ValueAmount value);
    void RemitTo(IAccount account, ValueAmount value);
    int GetCommissionDay();
    void RemoveWithdrawLimit();
    void EvaluateCommission();
    void AccrueCommission();
    void AddTransaction(ITransaction transaction);
    IReadOnlyList<ITransaction> GetTransactions();
}