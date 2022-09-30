using Shops.Entities;
using Shops.Models;
using Shops.Tools;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;
    private readonly List<Client> _clients;
    private readonly List<Product> _products;

    public ShopManager()
    {
        _shops = new List<Shop>();
        _clients = new List<Client>();
        _products = new List<Product>();
    }

    public Client AddClient(string clientName, decimal moneyAmount)
    {
        var client = new Client(Guid.NewGuid(), clientName, moneyAmount);
        _clients.Add(client);
        return client;
    }

    public Shop CreateShop(string name, Address address)
    {
        var shop = new Shop(Guid.NewGuid(), name, address);
        _shops.Add(shop);
        return shop;
    }

    public Product RegisterProduct(string name)
    {
        var product = new Product(Guid.NewGuid(), name);
        _products.Add(product);
        return product;
    }

    public void AddProduct(Product product, Shop shop, decimal price, int quantity)
        => shop.AddProductCard(product, price, quantity);

    public IReadOnlyList<ProductCard> GetShopCatalog(Shop shop)
        => shop.GetProductCardList();
    public Shop? FindShop(Guid id)
        => _shops.SingleOrDefault(s => s.Id.Equals(id));

    public Client? FindClient(Guid id)
        => _clients.SingleOrDefault(c => c.Id.Equals(id));

    public ProductQuantity AddProductQuantityPair(Product product, int quantity)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (!_products.Contains(product))
        {
            throw ShopManagerLogicException.NotExistingProduct(product);
        }

        return new ProductQuantity(product, quantity);
    }

    public void ChangeProductPrice(Shop shop, Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(product);
        if (!_shops.Contains(shop))
        {
            throw ShopManagerLogicException.NotExistingShop(shop);
        }

        if (!_products.Contains(product))
        {
            throw ShopManagerLogicException.NotExistingProduct(product);
        }

        shop.SetNewProductPrice(product, newPrice);
    }

    public void BuyAllFromProductList(Client client, Shop shop, List<ProductQuantity> productList)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(productList);
        if (!_clients.Contains(client))
        {
            throw ShopManagerLogicException.NotExistingClient(client);
        }

        if (!_shops.Contains(shop))
        {
            throw ShopManagerLogicException.NotExistingShop(shop);
        }

        if (productList
            .Select(pr => pr.Product)
            .Any(product => !_products.Contains(product)))
        {
            throw ShopManagerLogicException.NotExistingProductList();
        }

        shop.CheckProductPresence(productList);
        client.ChangeClientMoney(shop.GetTotalPrice(productList));
        client.AddProductsToProductHistory(shop.Purchase(productList));
    }

    public Shop? FindPriceOptimalShop(List<ProductQuantity> productList)
    {
        ArgumentNullException.ThrowIfNull(productList);
        decimal minTotalPrice = decimal.MaxValue;
        Shop? shopWithMinTotalPrice = null;
        foreach (Shop shop in _shops)
        {
            try
            {
                shop.CheckProductPresence(productList);
                decimal tempTotalPrice = shop.GetTotalPrice(productList);
                if (tempTotalPrice >= minTotalPrice) continue;
                minTotalPrice = tempTotalPrice;
                shopWithMinTotalPrice = shop;
            }
            catch (ShopLogicException)
            {
            }
        }

        return shopWithMinTotalPrice;
    }
}