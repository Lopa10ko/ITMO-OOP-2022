namespace Banks.Tools;

public class BanksException : Exception
{
    public BanksException(string errorMessage)
        : base(errorMessage) { }
}