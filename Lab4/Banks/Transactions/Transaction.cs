using Banks.Accounts;
using Banks.Entities;

namespace Banks.Transactions;

public abstract class Transaction : ITransaction
{
    public Transaction(IAccount actor, IAccount recipient, ValueAmount value)
    {
        Actor = actor;
        Recipient = recipient;
        Value = value;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public IAccount Actor { get; }
    public IAccount Recipient { get; }
    public ValueAmount Value { get; }
}