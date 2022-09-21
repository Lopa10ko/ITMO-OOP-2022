namespace Shops.Entities;

public class Client : IEquatable<Client>
{
    private List<Product> _cart;

    public Client(int idClient, string clientName, decimal moneyAmount)
    {
        Name = clientName;
        MoneyBank = moneyAmount;
        Id = idClient;
        _cart = new List<Product>();
    }

    public string Name { get; }
    public decimal MoneyBank { get; }
    public int Id { get; }

    public IReadOnlyList<Product> GetProducts => _cart;

    public bool Equals(Client? other)
        => other is not null && MoneyBank.Equals(other.MoneyBank) && Id.Equals(other.Id);

    public override bool Equals(object? obj)
        => Equals(obj as Client);

    public override int GetHashCode()
        => HashCode.Combine(MoneyBank, Id);
}