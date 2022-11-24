using Banks.Accounts;
using Banks.Models;
using Banks.Services;
using Banks.TimeManager;

namespace Banks.Banks;

public class Bank : IEquatable<Bank>
{
    private readonly List<IClient> _clients;
    private readonly List<IAccount> _accounts;

    public Bank(string name, BankInfo bankInfo)
    {
        _clients = new List<IClient>();
        _accounts = new List<IAccount>();
        Name = name;
        BankInfo = bankInfo;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }
    public BankInfo BankInfo { get; set; }

    public void CreateDebitAccount(IClient client, decimal balance)
    {
        var account = new DebitAccount(balance, BankInfo, client.IsVerified);
        _accounts.Add(account);
        if (!_clients.Contains(client))
            _clients.Add(client);
        client.AddAccount(account);
    }

    public void Notify(DateOnly currentDate)
    {
        foreach (IAccount account in _accounts)
        {
            account.EvaluateCommission();
            if (currentDate.Day.Equals(account.GetCommissionPeriod()))
                account.AccrueCommission();
        }
    }

    public void CreateTransaction(Guid actorId, Guid recipientId, decimal value)
    {
        IAccount actorAccount = GetActor(actorId);
        IAccount recipientAccount = GetActor(recipientId);
        var transaction = new TransferTransaction(actorAccount, recipientAccount, new ValueAmount(value));
        actorAccount.AddTransaction(transaction);
        recipientAccount.AddTransaction(transaction);
    }

    public void CreateTransaction(Guid actorId, IAccount recipientAccount, decimal value)
    {
        IAccount actorAccount = GetActor(actorId);
        var transaction = new TransferTransaction(actorAccount, recipientAccount, new ValueAmount(value));
        actorAccount.AddTransaction(transaction);
        recipientAccount.AddTransaction(transaction);
    }

    public void CreateWithdrawTransaction(Guid actorId, decimal value)
    {
        IAccount actorAccount = GetActor(actorId);
        var transaction = new WithdrawTransaction(actorAccount, new ValueAmount(value));
        actorAccount.AddTransaction(transaction);
    }

    public void CreateReplenishmentTransaction(Guid actorId, decimal value)
    {
        IAccount actorAccount = GetActor(actorId);
        var transaction = new ReplenishmentTransaction(actorAccount, new ValueAmount(value));
        actorAccount.AddTransaction(transaction);
    }

    public bool Equals(Bank? other) => other is not null && Id.Equals(other.Id);
    public override bool Equals(object? obj) => Equals(obj as Bank);
    public override int GetHashCode() => Id.GetHashCode();
    public IAccount GetActor(Guid id)
        => _accounts.FirstOrDefault(account => account.Id.Equals(id)) ?? throw new Exception();

    private static int ValidateCommissionDay(int commissionDay, DateOnly currentDate)
    {
        if (commissionDay > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
            throw new Exception();
        return commissionDay;
    }
}