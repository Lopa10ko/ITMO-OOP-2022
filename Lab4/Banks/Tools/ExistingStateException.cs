using Banks.Banks;
using Banks.Clients;

namespace Banks.Tools;

public class ExistingStateException : BanksException
{
    private ExistingStateException(string errorMessage)
        : base(errorMessage) { }

    public static ExistingStateException ExistingBank(Bank bank)
        => new ExistingStateException($"Bank ({bank.Name} - {bank.Id}) is already in Central Bank");

    public static ExistingStateException ExistingClient(Bank bank)
        => new ExistingStateException($"Client is already in observers list of bank {bank.Id}");

    public static ExistingStateException NonExistingClient(Bank bank)
        => new ExistingStateException($"Client is not in observers list of bank {bank.Id} to remove");
}