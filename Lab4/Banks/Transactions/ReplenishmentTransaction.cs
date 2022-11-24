using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public class ReplenishmentTransaction : OneSidedTransaction
{
    public ReplenishmentTransaction(IAccount actor, ValueAmount value)
        : base(actor, value)
    {
        actor.IncreaseMoneyValue(value);
    }
}