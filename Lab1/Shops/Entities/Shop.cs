using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class Shop : IEquatable<Shop>
{
    private List<ProductCard> _products;
    public Shop(Guid id, string shopName, Address address)
    {
        Id = id;
        Name = shopName;
        Address = address;
        _products = new List<ProductCard>();
    }

    public string Name { get; }
    public Guid Id { get; }
    public Address Address { get; }

    public bool Equals(Shop? other)
        => other is not null && Id.Equals(other.Id);

    public override bool Equals(object? obj)
        => Equals(obj as Shop);

    public override int GetHashCode()
        => HashCode.Combine(Id, Address);

    public void CheckProductPresence(List<ProductQuantity> productList)
    {
        IEnumerable<Product> products = productList.Select(pr => pr.Product);
        if (products.Any(product => !IsProductInShop(product)))
        {
            throw ShopLogicException.InvalidProductQuantity(this);
        }

        bool checkInvalidProductQuantity = productList.Any(pq => _products.Where(pc => pq.Product.Equals(pc.Product)).Any(pc => pq.Quantity > pc.Quantity));
        if (checkInvalidProductQuantity)
        {
            throw ShopLogicException.InvalidProductQuantity(this);
        }
    }

    internal void AddProductCard(Product product, decimal price, int quantity)
    {
        if (IsProductInShop(product))
        {
            _products
                .Single(pc => pc.Product.Equals(product))
                .SetNewQuantity(quantity);
        }
        else
        {
            _products.Add(new ProductCard(product, price, quantity));
        }
    }

    internal IReadOnlyList<ProductCard> GetProductCardList() => _products;

    internal decimal GetTotalPrice(List<ProductQuantity> productList)
    {
        return productList
            .Sum(pq => _products.Where(pc => pc.Product.Equals(pq.Product))
                .Sum(pc => pc.Price * pq.Quantity));
    }

    internal List<Product> Purchase(Client client, List<ProductQuantity> productList)
    {
        CheckProductPresence(productList);
        client.ChangeClientMoney(GetTotalPrice(productList));
        foreach (ProductQuantity pq in productList)
        {
            foreach (ProductCard pc in _products.Where(pc => pc.Product.Equals(pq.Product)))
            {
                pc.SetNewQuantity(-pq.Quantity);
            }
        }

        return productList.Select(pq => pq.Product).ToList();
    }

    internal void SetNewProductPrice(Product product, decimal newPrice)
    {
        if (!IsProductInShop(product))
            throw ShopLogicException.ProductNotInShop(this, product);
        _products.Single(pc => pc.Product.Equals(product)).Price = newPrice;
    }

    private bool IsProductInShop(Product product)
        => _products.Any(pc => pc.Product.Equals(product));
}