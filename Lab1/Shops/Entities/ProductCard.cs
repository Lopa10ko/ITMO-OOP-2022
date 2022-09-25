using Shops.Models;
using Shops.Tools;

namespace Shops.Entities;

public class ProductCard : IEquatable<ProductCard>
{
    private const int MinIntValue = 0;
    private const decimal MinDecimalValue = 0;
    private int _quantity;
    private decimal _price;
    public ProductCard(Product product, decimal price = 0, int quantity = 0)
    {
        Product = product;
        Price = price;
        Quantity = quantity;
    }

    public decimal Price
    {
        get => _price;
        private set
        {
            if (value < MinDecimalValue)
                throw ValueException.InvalidPriceValue(this, value);
            _price = value;
        }
    }

    public int Quantity
    {
        get => _quantity;
        private set
        {
            if (value < MinIntValue)
                throw ValueException.InvalidQuantityValue(this, value);
            _quantity = value;
        }
    }

    public Product Product { get; }

    public bool Equals(ProductCard? other)
        => other is not null && Product.Equals(other.Product);

    public override bool Equals(object? obj)
        => Equals(obj as ProductCard);

    public override int GetHashCode()
        => Product.GetHashCode();

    internal void SetNewQuantity(int quantity)
        => Quantity += quantity;

    internal void SetNewPrice(decimal price)
        => Price = price;
}