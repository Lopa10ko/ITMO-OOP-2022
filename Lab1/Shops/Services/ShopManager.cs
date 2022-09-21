using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public class ShopManager : IShopManager
{
    private readonly List<Shop> _shops;
    private readonly List<Client> _clients;
    private IdGenerator _id;

    public ShopManager()
    {
        _shops = new List<Shop>();
        _clients = new List<Client>();
        _id = new IdGenerator();
    }

    public Client AddClient(string clientName, decimal moneyAmount)
    {
        throw new NotImplementedException();
    }

    public Shop CreateShop(ShopName name, Address address)
    {
        throw new NotImplementedException();
    }

    public Product AddProduct(Shop shop, decimal price, uint quantity)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Product> GetShopCatalog(Shop shop)
    {
        throw new NotImplementedException();
    }

    public Shop? FindShopAddress(int id)
    {
        throw new NotImplementedException();
    }

    public void ChangeProductPrice(Shop shop, Product product, decimal newPrice)
    {
        throw new NotImplementedException();
    }
}