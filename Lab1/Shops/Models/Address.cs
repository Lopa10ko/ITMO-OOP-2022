using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Models;

public record Address
{
    private const string AddressPattern = @"^[A-Za-z\-.]*, [0-9]*(, [\w]*)?$";
    public Address(string address)
    {
        ValidateAddress(address);
        AddressString = address;
    }

    public string AddressString { get; }

    public override int GetHashCode()
        => AddressString.GetHashCode();

    private void ValidateAddress(string address)
    {
        if (!Regex.IsMatch(address, AddressPattern))
            throw AddressException.Create(address);
    }
}