using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    private const string NamePattern = @"^[a-z\-.,0-9\s]*$";
    public Product(Guid id, string name)
    {
        ValidateName(name);
        Name = name;
        Id = id;
    }

    public string Name { get; }

    public Guid Id { get; }

    public bool Equals(Product? other)
        => other is not null && Id.Equals(other.Id);

    public override bool Equals(object? obj)
        => Equals(obj as Product);

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Id);
    }

    private static void ValidateName(string name)
    {
        if (!Regex.IsMatch(name, NamePattern, RegexOptions.Compiled | RegexOptions.IgnoreCase))
            throw NamingException.Create(name);
    }
}