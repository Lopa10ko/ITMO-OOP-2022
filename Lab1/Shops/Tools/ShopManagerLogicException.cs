using Shops.Entities;

namespace Shops.Tools;

public class ShopManagerLogicException : ShopsException
{
    private ShopManagerLogicException(string errorMessage)
        : base(errorMessage) { }

    public static ShopManagerLogicException NotExistingClient(Client client)
        => new ($"Invalid use: Client {client.Name} is not registered");

    public static ShopManagerLogicException NotExistingShop(Shop shop)
        => new ($"Invalid use: Shop {shop.Name} is not registered");

    public static ShopManagerLogicException NotExistingProduct(Product product)
        => new ($"Invalid use: Product {product.Name} is not registered");

    public static ShopManagerLogicException NotExistingProductList()
        => new ($"Invalid use: one of the products in ProductList is not registered");
}