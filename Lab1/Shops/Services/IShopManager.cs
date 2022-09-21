using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    Client AddClient(string clientName, decimal moneyAmount);

    Shop CreateShop(ShopName name, Address address);

    Product AddProduct(Shop shop, decimal price, uint quantity);

    IReadOnlyList<Product> GetShopCatalog(Shop shop);

    Shop? FindShopAddress(int id);

    void ChangeProductPrice(Shop shop, Product product, decimal newPrice);
}