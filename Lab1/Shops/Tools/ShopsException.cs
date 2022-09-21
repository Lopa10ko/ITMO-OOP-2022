namespace Shops.Tools;

public class ShopsException : Exception
{
    public ShopsException(string errorMessage)
        : base(errorMessage) { }
}