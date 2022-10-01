using Shops.Entities;
using Shops.Models;

namespace Shops.Tools;

public class ValueException : Exception
{
    private ValueException(string errorMessage)
        : base(errorMessage) { }

    public static ValueException InvalidPriceValue(ProductCard productCard, decimal value)
        => new ($"Product {productCard.Product.Name} can't have price of {value}");

    public static ValueException InvalidQuantityValue(ProductCard productCard, int value)
        => new ($"Product {productCard.Product.Name} can't have quantity of {value}");

    public static ValueException InvalidQuantityValue(ProductQuantity productQuantity, int value)
        => new ($"Product {productQuantity.Product.Name} can't have quantity of {value}");
}