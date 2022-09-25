using Shops.Entities;

namespace Shops.Tools;

public class MoneyBankException : ShopsException
{
    private MoneyBankException(string errorMessage)
        : base(errorMessage) { }

    public static MoneyBankException InvalidMoneyBankState(Client client)
        => new ($"{client.Id} - {client.Name} has total amount of {client.MoneyBank}");

    public static MoneyBankException InvalidMoneyBankValue(decimal moneyAmount)
        => new ($"Invalid MoneyBank has total amount of {moneyAmount}");
}