using Shops.Models;

namespace Shops.Tools;

public class AddressException : ShopsException
{
    public AddressException(string errorMessage)
        : base(errorMessage) { }

    public static AddressException Create(string address)
        => new AddressException($"Address {address} is not formatted correctly");
}