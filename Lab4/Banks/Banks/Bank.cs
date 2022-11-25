using System.Transactions;
using Banks.Accounts;
using Banks.Clients;
using Banks.Entities;
using Banks.TimeManager;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.Banks;

public class Bank : IEquatable<Bank>, IObservableObject
{
    private readonly List<IClient> _clients;
    private readonly List<IObserverObject> _observerClients;
    private readonly List<IAccount> _accounts;

    public Bank(string name, BankInfo bankInfo)
    {
        _clients = new List<IClient>();
        _observerClients = new List<IObserverObject>();
        _accounts = new List<IAccount>();
        Name = name;
        BankInfo = bankInfo;
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }
    public BankInfo BankInfo { get; private set; }
    public IReadOnlyCollection<IAccount> GetAccounts => _accounts.AsReadOnly();

    public Guid CreateAccount<TAccount>(IClient client, decimal balance)
        where TAccount : IAccount
    {
        var accountFactory = new BankAccountFactory(balance, BankInfo, client.IsVerified);
        IAccount account = accountFactory.CreateAccount<TAccount>();
        _accounts.Add(account);
        if (!_clients.Contains(client))
            _clients.Add(client);
        client.AddAccount(account);
        return account.Id;
    }

    public void UpdateBankInfo(BankInfo bankInfo)
    {
        BankInfo = bankInfo;
        NotifyClients();
    }

    public void Notify(DateOnly currentDate)
    {
        foreach (IAccount account in _accounts)
        {
            account.EvaluateCommission();
            if (currentDate.Day.Equals(ValidateCommissionDay(account.GetCommissionDay(), currentDate)))
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
        => _accounts.FirstOrDefault(account => account.Id.Equals(id))
           ?? throw AlienEntityException.InvalidAccount(id);

    public void AddObserverClient(IObserverObject client)
    {
        if (_observerClients.Contains(client))
            throw ExistingStateException.ExistingClient(this);
        _observerClients.Add(client);
    }

    public void RemoveObserverClient(IObserverObject client)
    {
        if (!_observerClients.Contains(client))
            throw ExistingStateException.ExistingClient(this);
        _observerClients.Remove(client);
    }

    public void NotifyClients()
    {
        foreach (IObserverObject client in _observerClients)
        {
            client.Update(BankInfo.ToString());
        }
    }

    private static int ValidateCommissionDay(int commissionDay, DateOnly currentDate)
    {
        if (commissionDay > DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
            throw CommissionDayException.InvalidCommissionDay(commissionDay, currentDate);
        return commissionDay;
    }
}