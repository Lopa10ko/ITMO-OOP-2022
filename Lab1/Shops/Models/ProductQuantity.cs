using Shops.Entities;
using Shops.Tools;

namespace Shops.Models;

public record ProductQuantity
{
    private const int MinProductQuantity = 0;
    private int _quantity;
    public ProductQuantity(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public int Quantity
    {
        get => _quantity;
        private set
        {
            if (value < MinProductQuantity)
                throw ValueException.InvalidQuantityValue(this, value);
            _quantity = value;
        }
    }

    public Product Product { get; }
}