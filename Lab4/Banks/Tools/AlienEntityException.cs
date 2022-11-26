namespace Banks.Tools;

public class AlienEntityException : BanksException
{
    private AlienEntityException(string errorMessage)
        : base(errorMessage) { }

    public static AlienEntityException InvalidAccount(Guid id)
        => new AlienEntityException($"Invalid or non-existing account with Guid ({id})");

    public static AlienEntityException InvalidBank(Guid id)
        => new AlienEntityException($"No bank for account with Guid ({id})");

    public static AlienEntityException InvalidAccountType(Type type)
        => new AlienEntityException($"Invalid account type ({type}), should be Deposit/Debit/Credit");
}