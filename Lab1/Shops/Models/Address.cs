using Shops.Tools;

namespace Shops.Models;

public record Address
{
    private const int MinAddressLength = 100;
    public Address(string address)
    {
        ValidateAddress(address);
        AddressString = address;
    }

    public string AddressString { get; }

    public override int GetHashCode()
        => AddressString.GetHashCode();

    private static void ValidateAddress(string address)
    {
        if (address.Length > MinAddressLength)
            throw NamingException.InvalidName(address);
    }
}