using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopManagerTests
{
    private readonly ShopManager _shopManager = new ShopManager();

    public static IEnumerable<object[]> ManageClientOrderAllFromShopData()
    {
        yield return new object[]
        {
            "H&M", "St.Petersburg, Mira, 78 a", "Gosha Lopatenko", 1300m,
            new List<Tuple<string, int, decimal>> { new ("Shirt", 2, 400.01m), new ("Pants", 1, 400.09m) },
        };
        yield return new object[]
        {
            "Adidas Co.", "Moscow, New Arbat, 11", "Losha Gopatenko", 14000m,
            new List<Tuple<string, int, decimal>> { new ("Shirt Adidas", 2, 2999.9m), new ("Pants Adidas", 2, 3999.99m) },
        };
        yield return new object[]
        {
            "Rubber Pigs", "Saint-Petersburg, Main Ave., 9", "Gosha Kruglov", 1500m,
            new List<Tuple<string, int, decimal>> { new ("Small Honking Pig", 2, 239.01m), new ("Big Honking Pig", 4, 239.02m) },
        };
    }

    public static IEnumerable<object[]> CreateShopChangeProductPriceData()
    {
        yield return new object[]
        {
            "Huff-and-Puff", "Moscow, Pykhtelnaya, 69", "Tropic Ice", 2, 4000m, 1000m,
        };
        yield return new object[]
        {
            "Kotelnaya", "Orel, Dymnaya, 420", "Orange Soda", 30, 1000m, 5000m,
        };
    }

    [Theory]
    [MemberData(nameof(ManageClientOrderAllFromShopData))]
    public void ManageClientOrderAllFromShop_SuccessfulPurchase(
        string shopName,
        string address,
        string clientName,
        decimal moneyAmount,
        List<Tuple<string, int, decimal>> valuesSupplyProductQuantity)
    {
        Shop testShop = _shopManager.CreateShop(shopName, new Address(address));
        Client testClient = _shopManager.AddClient(clientName, moneyAmount);
        var productList = new List<ProductQuantity>();
        decimal totalPrice = 0m;
        foreach (Tuple<string, int, decimal> productInfo in valuesSupplyProductQuantity)
        {
            Product registeredProduct = _shopManager.RegisterProduct(productInfo.Item1);
            _shopManager.AddProduct(registeredProduct, testShop, productInfo.Item3, productInfo.Item2);
            productList.Add(new ProductQuantity(registeredProduct, productInfo.Item2));
            totalPrice += productInfo.Item2 * productInfo.Item3;
        }

        _shopManager.BuyAllFromProductList(testClient, testShop, productList);
        IReadOnlyList<ProductCard> testShopCatalogAfter = _shopManager.GetShopCatalog(testShop);
        Assert.Equal(moneyAmount - totalPrice, testClient.MoneyBank);
        Assert.Equal(0, testShopCatalogAfter.Select(pc => pc.Quantity).Sum());
    }

    [Theory]
    [MemberData(nameof(CreateShopChangeProductPriceData))]
    public void CreateShopChangeProductPrice_PriceChanged(
        string shopName,
        string shopAddress,
        string productName,
        int productQuantity,
        decimal initPrice,
        decimal newPrice)
    {
        Shop testShop = _shopManager.CreateShop(shopName, new Address(shopAddress));
        Product testProduct = _shopManager.RegisterProduct(productName);
        _shopManager.AddProduct(testProduct, testShop, initPrice, productQuantity);
        Assert.Equal(initPrice, _shopManager.GetShopCatalog(testShop)[0].Price);
        _shopManager.ChangeProductPrice(testShop, testProduct, newPrice);
        Assert.Equal(newPrice, _shopManager.GetShopCatalog(testShop)[0].Price);
    }

    [Fact]
    public void SomeTests()
    {
        Client testClient = _shopManager.AddClient("Ivan", 10000m);
        Shop testShop = _shopManager.CreateShop("Rubber Pigs", new Address("St.Petersburg, Mira, 78 a"));
        Product testProduct1 = _shopManager.RegisterProduct("Big Pig");
        Product unregistered = _shopManager.RegisterProduct("ddd");
        var unregisteredProduct = new Product(Guid.NewGuid(), "Big Pig");
        Assert.NotEqual(testProduct1, unregisteredProduct);
        Product testProduct2 = _shopManager.RegisterProduct("Fat ass");
        _shopManager.AddProduct(testProduct1, testShop, 400m, 2);
        Assert.Equal(400m, _shopManager.GetShopCatalog(testShop)[0].Price);
        _shopManager.ChangeProductPrice(testShop, testProduct1, 500m);
        Assert.Equal(500m, _shopManager.GetShopCatalog(testShop)[0].Price);
        _shopManager.AddProduct(testProduct1, testShop, 400m, 3);
        Assert.Equal(5, _shopManager.GetShopCatalog(testShop)[0].Quantity);
        _shopManager.AddProduct(testProduct2, testShop, 300m, 5);

        Assert.Equal(10, _shopManager.GetShopCatalog(testShop).Select(card => card.Quantity).Sum());
        var productList = new List<ProductQuantity> { _shopManager.AddProductQuantityPair(testProduct1, 2), _shopManager.AddProductQuantityPair(testProduct2, 1) };
        _shopManager.BuyAllFromProductList(testClient, testShop, productList);
        Assert.Equal(7, _shopManager.GetShopCatalog(testShop).Select(card => card.Quantity).Sum());
    }
}