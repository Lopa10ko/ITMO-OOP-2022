using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Banks.Accounts;
using Banks.Banks;
using Banks.Entities;
using Banks.Models;
using Xunit;
using Xunit.Abstractions;

namespace Banks.Test;

public class BanksTest
{
    [Fact]
    public void Test1()
    {
        int daysInYear = new GregorianCalendar().GetDaysInYear(DateTime.Now.Year);
        Client client = Client.Builder
            .WithName("Losha")
            .WithLastName("Gopatenko").Build();
        client.Address = "Kronverskii, 49";
        client.Passport = "4017123456";

        DepositRules depositRules = DepositRules.Builder
            .AddDepositRule(new DepositRule(20000m, 3))
            .AddDepositRule(new DepositRule(50000m, 5))
            .AddDepositRule(new DepositRule(100000m, 8))
            .AddDepositRule(new DepositRule(100000.01m, 10))
            .Build();
        var centralBank = CentralBank.GetInstance();
        Bank spermBank = centralBank.CreateBank("Sperm", new BankInfo(30, 10, 8.5m / daysInYear, 10000m, 50000m, 20m / daysInYear, depositRules));
        spermBank.CreateDebitAccount(client, 1000000m);
    }
}