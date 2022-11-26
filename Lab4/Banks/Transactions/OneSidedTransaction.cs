using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public abstract class OneSidedTransaction : ITransaction
{
    public OneSidedTransaction(IAccount actor, ValueAmount value)
    {
        Actor = actor;
        Value = value;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public IAccount Actor { get; }
    public ValueAmount Value { get; }
}