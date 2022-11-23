using Banks.Accounts;
using Banks.Banks;

namespace Banks.Models;

public class Client : IEquatable<Client>, IClient
{
    private readonly List<IAccount> _accounts;
    private readonly List<IAccount> _accountsObservable;
    private string _address = string.Empty;
    private string _passport = string.Empty;
    private Client(string name, string lastName, string address, string passport)
    {
        Name = name;
        LastName = lastName;
        Address = address;
        Passport = passport;
        Id = Guid.NewGuid();
        _accountsObservable = new List<IAccount>();
        _accounts = new List<IAccount>();
    }

    public static INameBuilder Builder => new ClientBuilder();
    public Guid Id { get; }
    public string Name { get; }
    public string LastName { get; }
    public bool IsVerified => !(string.IsNullOrEmpty(Passport) || string.IsNullOrEmpty(Address));

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            if (IsVerified)
                Notify();
        }
    }

    public string Passport
    {
        get => _passport;
        set
        {
            _passport = value;
            if (IsVerified)
                Notify();
        }
    }

    public void AddAccount(IAccount account)
    {
        if (!IsVerified)
            _accountsObservable.Add(account);
        _accounts.Add(account);
    }

    public bool Equals(Client? other) => other is not null && Id.Equals(other.Id);

    public override bool Equals(object? obj) => Equals(obj as Client);

    public override int GetHashCode() => Id.GetHashCode();

    private void Notify()
    {
        foreach (IAccount account in _accountsObservable)
        {
            account.RemoveWithdrawLimit();
        }
    }

    public class ClientBuilder : INameBuilder, ILastNameBuilder, IChainBuilder
    {
        public ClientBuilder()
        {
            Name = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Passport = string.Empty;
        }

        private string Name { get; set; }
        private string LastName { get; set; }
        private string Address { get; set; }
        private string Passport { get; set; }

        public IChainBuilder PlugBuilder() => this;

        public Client Build()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(LastName))
                throw new Exception();
            return new Client(Name, LastName, Address, Passport);
        }

        public IChainBuilder WithAddress(string address)
        {
            Address = address;
            return this;
        }

        public IChainBuilder WithPassport(string passport)
        {
            Passport = passport;
            return this;
        }

        public ILastNameBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public IChainBuilder WithLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
    }
}

public interface INameBuilder
{
    ILastNameBuilder WithName(string name);
}

public interface ILastNameBuilder
{
    IChainBuilder WithLastName(string lastName);
}

public interface IChainBuilder
{
    IChainBuilder WithAddress(string address);
    IChainBuilder WithPassport(string passport);
    Client Build();
}