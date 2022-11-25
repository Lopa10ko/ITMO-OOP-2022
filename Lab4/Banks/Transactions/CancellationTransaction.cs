using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public class CancellationTransaction : Transaction
{
    public CancellationTransaction(IAccount actor, IAccount recipient, ValueAmount value)
        : base(actor, recipient, value)
    {
        actor.RemitTo(recipient, value);
    }
}