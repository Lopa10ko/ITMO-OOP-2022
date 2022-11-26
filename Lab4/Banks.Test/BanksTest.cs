using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Entities;
using Banks.Tools;
using Xunit;
using Xunit.Abstractions;

namespace Banks.Test;

public class BanksTest
{
    private DepositRules _defaultDepositRules;
    private List<IClient> _testClients;
    private decimal _daysInYear = new GregorianCalendar().GetDaysInYear(DateTime.Now.Year);
    public BanksTest()
    {
        Client client1 = Client.Builder
            .WithName("Losha")
            .WithLastName("Gopatenko")
            .Build();
        client1.Address = "Kronverskii, 49";
        client1.Passport = "2282281337";
        Client client2 = Client.Builder
            .WithName("Gosha")
            .WithLastName("Lopatenko")
            .WithAddress("Lomonosova, 7")
            .WithPassport("4017123654")
            .Build();
        Client client3 = Client.Builder
            .WithName("Gosha")
            .WithLastName("Kruglov")
            .Build();
        client3.Address = "Earth";

        _testClients = new List<IClient> { client1, client2, client3 };
        _defaultDepositRules = DepositRules.Builder
            .AddDepositRule(new DepositRule(20000m, 3m / _daysInYear))
            .AddDepositRule(new DepositRule(50000m, 5m / _daysInYear))
            .AddDepositRule(new DepositRule(100000m, 8m / _daysInYear))
            .AddDepositRule(new DepositRule(100000.01m, 10m / _daysInYear))
            .Build();
    }

    [Fact]
    public void PerformTransactionWithVerificationCheck_CatchUnsuccessfulTransaction()
    {
        var centralBank = CentralBank.GetInstance();
        Bank spermBank1 = centralBank.CreateBank("Sperm", new BankInfo(30, 10, 8.5m / _daysInYear, 10000m, 50000m, 1000m, _defaultDepositRules));
        Bank spermBank2 = centralBank.CreateBank("Sperm", new BankInfo(30, 10, 8.5m / _daysInYear, 10000m, 50000m, 1000m, _defaultDepositRules));
        Guid clientVerifiedDepositId = spermBank1.CreateAccount<DepositAccount>(_testClients[0], 50000m);
        Assert.True(_testClients[0].IsVerified);
        Guid clientUnverifiedDebitId = spermBank2.CreateAccount<DebitAccount>(_testClients[2], 1000000m);
        Assert.False(_testClients[2].IsVerified);
        Assert.Throws<ValueAmountException>(() => centralBank.CreateTransaction(clientVerifiedDepositId, clientUnverifiedDebitId, 50001m));
    }

    [Fact]
    public void AggregateTimeManagerCheckCommission_CommissionAccrued()
    {
        var centralBank = CentralBank.GetInstance();
        Bank spermBank = centralBank.CreateBank("Sperm", new BankInfo(30, 10, 8.5m / _daysInYear, 10000m, 50000m, 1000m, _defaultDepositRules));
        Guid depositId = spermBank.CreateAccount<DepositAccount>(_testClients[0], 50000m);
        Guid debitId = spermBank.CreateAccount<DebitAccount>(_testClients[0], 1000000m);
        Guid creditId = spermBank.CreateAccount<CreditAccount>(_testClients[0], -1m);
        string? moneyBankBefore = spermBank.GetAccounts.First(account => account.Id.Equals(depositId)).ToString();
        centralBank.TimeMachine.SkipDays(spermBank.BankInfo.CommissionDay);
        string? moneyBankAfter = spermBank.GetAccounts.First(account => account.Id.Equals(depositId)).ToString();
        Assert.NotEqual(moneyBankBefore, moneyBankAfter);
    }

    [Fact]
    public void PerformOneSidedTransaction_CheckSuccessfulTransaction()
    {
        var centralBank = CentralBank.GetInstance();
        Bank spermBank = centralBank.CreateBank("Sperm", new BankInfo(30, 10, 8.5m / _daysInYear, 10000m, 50000m, 1000m, _defaultDepositRules));
        Guid creditId = spermBank.CreateAccount<CreditAccount>(_testClients[0], 500m);
        string? moneyBankBefore = spermBank.GetAccounts.First(account => account.Id.Equals(creditId)).ToString();
        spermBank.CreateWithdrawTransaction(creditId, 5000);
        string? moneyBankAfter = spermBank.GetAccounts.First(account => account.Id.Equals(creditId)).ToString();
        Assert.NotEqual(moneyBankBefore, moneyBankAfter);
        Guid debitId = spermBank.CreateAccount<DebitAccount>(_testClients[2], 100m);
        Assert.Throws<ValueAmountException>(() => spermBank.CreateWithdrawTransaction(debitId, 101m));
    }
}