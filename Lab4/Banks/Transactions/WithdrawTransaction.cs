using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public class WithdrawTransaction : OneSidedTransaction
{
    public WithdrawTransaction(IAccount actor, ValueAmount value)
        : base(actor, value)
    {
        actor.DecreaseMoneyValue(value);
    }
}