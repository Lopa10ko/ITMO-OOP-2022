using Banks.Accounts;

namespace Banks.Entities;

public interface ITransaction
{
}

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

public class TransferTransaction : Transaction
{
    public TransferTransaction(IAccount actor, IAccount recipient, ValueAmount value)
        : base(actor, recipient, value)
    {
        actor.RemitTo(recipient, value);
    }
}

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

public class WithdrawTransaction : OneSidedTransaction
{
    public WithdrawTransaction(IAccount actor, ValueAmount value)
        : base(actor, value)
    {
        actor.DecreaseMoneyValue(value);
    }
}

public class ReplenishmentTransaction : OneSidedTransaction
{
    public ReplenishmentTransaction(IAccount actor, ValueAmount value)
        : base(actor, value)
    {
        actor.IncreaseMoneyValue(value);
    }
}