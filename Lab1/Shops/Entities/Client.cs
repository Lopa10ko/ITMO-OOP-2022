﻿using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class Client : IEquatable<Client>
{
    private const decimal MinMoneyAmount = 0;
    private List<Product> _productHistory;

    public Client(Guid idClient, string clientName, decimal moneyAmount)
    {
        Name = clientName;
        MoneyBank = moneyAmount;
        Id = idClient;
        _productHistory = new List<Product>();
    }

    public string Name { get; }

    public decimal MoneyBank { get; private set; }

    public Guid Id { get; }

    public IReadOnlyList<Product> GetProductHistory() => _productHistory;

    public bool Equals(Client? other)
        => other is not null && MoneyBank.Equals(other.MoneyBank) && Id.Equals(other.Id);

    public override bool Equals(object? obj)
        => Equals(obj as Client);

    public override int GetHashCode()
        => Id.GetHashCode();

    internal void ChangeClientMoney(decimal totalPrice)
    {
        ValidateMoneyAmount(MoneyBank - totalPrice);
        MoneyBank -= totalPrice;
    }

    internal void AddProductsToProductHistory(List<Product> products)
        => _productHistory.AddRange(products);

    private static void ValidateMoneyAmount(decimal moneyAmount)
    {
        if (moneyAmount < MinMoneyAmount)
            throw MoneyBankException.InvalidMoneyBankValue(moneyAmount);
    }
}