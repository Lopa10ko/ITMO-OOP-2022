using System.Transactions;

namespace Banks.Accounts;

public interface IAccount
{
    void IncreaseMoneyValue(decimal value);
    void DecreaseMoneyValue(decimal value);
    void RemitTo(decimal value, IAccount account);
    int GetCommissionPeriod();
    void RemoveWithdrawLimit();
    void EvaluateCommission();
    void AccrueCommission();
    IReadOnlyList<Transaction> GetTransactions();
}