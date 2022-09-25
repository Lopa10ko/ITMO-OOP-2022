using Shops.Entities;

namespace Shops.Tools;

public class ShopLogicException : ShopsException
{
    private ShopLogicException(string errorMessage)
        : base(errorMessage) { }

    public static ShopLogicException InvalidProductQuantity(Shop shop)
        => new ($"{shop.Name} - {shop.Id} has supply problems");

    public static ShopLogicException ProductNotInShop(Shop shop, Product product)
        => new ($"There is no Product {product.Name} - {product.Id} in Shop {shop.Name} - {shop.Id}");
}