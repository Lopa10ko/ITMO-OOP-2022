using System.Text.RegularExpressions;
using Shops.Tools;

namespace Shops.Entities;

public class Product : IEquatable<Product>
{
    private static readonly Regex Validation = new Regex(@"^[a-z\-.,0-9\s]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
        if (!Validation.IsMatch(name))
            throw NamingException.InvalidName(name);
    }
}