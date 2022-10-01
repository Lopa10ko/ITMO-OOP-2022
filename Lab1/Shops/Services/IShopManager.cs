using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopManager
{
    Client AddClient(string clientName, decimal moneyAmount);

    Shop CreateShop(string name, Address address);

    Product RegisterProduct(string name);

    void AddProduct(Product product, Shop shop, decimal price, int quantity);

    IReadOnlyList<ProductCard> GetShopCatalog(Shop shop);

    Shop? FindShop(Guid id);

    Client? FindClient(Guid id);

    ProductQuantity AddProductQuantityPair(Product product, int quantity);

    void ChangeProductPrice(Shop shop, Product product, decimal newPrice);

    void BuyAllFromProductList(Client client, Shop shop, List<ProductQuantity> productList);

    Shop? FindPriceOptimalShop(List<ProductQuantity> productList);
}