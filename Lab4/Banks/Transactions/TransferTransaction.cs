using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public class TransferTransaction : Transaction
{
    public TransferTransaction(IAccount actor, IAccount recipient, ValueAmount value)
        : base(actor, recipient, value)
    {
        actor.RemitTo(recipient, value);
    }
}