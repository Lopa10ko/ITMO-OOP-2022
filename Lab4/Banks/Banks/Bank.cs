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

    public bool Equals(Bank? other) => other is not null && Id.Equals(other.Id);
    public override bool Equals(object? obj) => Equals(obj as Bank);
    public override int GetHashCode() => Id.GetHashCode();
}