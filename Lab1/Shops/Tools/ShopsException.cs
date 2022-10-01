namespace Shops.Tools;

public class ShopsException : Exception
{
    protected ShopsException(string errorMessage)
        : base(errorMessage) { }
}