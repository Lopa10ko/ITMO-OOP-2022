using Banks.Clients;
using Banks.Entities;
using Banks.Tools;

namespace Banks.Accounts;

public abstract class AccountFactory
{
    public abstract IAccount CreateAccount<TAccount>()
        where TAccount : IAccount;
}

public class BankAccountFactory : AccountFactory
{
    private BankInfo _bankInfo;
    private decimal _balance;
    private bool _isVerified;
    public BankAccountFactory(decimal balance, BankInfo bankInfo, bool isVerified)
    {
        _bankInfo = bankInfo;
        _balance = balance;
        _isVerified = isVerified;
    }

    public override IAccount CreateAccount<TAccount>()
    {
        Type typeAccount = typeof(TAccount);
        if (typeAccount == typeof(CreditAccount))
            return CreateCreditAccount();
        if (typeAccount == typeof(DebitAccount))
            return CreateDebitAccount();
        if (typeAccount == typeof(DepositAccount))
            return CreateDepositAccount();
        throw AlienEntityException.InvalidAccountType(typeAccount);
    }

    private IAccount CreateDepositAccount()
        => new DepositAccount(new ValueAmount(_balance), _bankInfo, _isVerified);

    private IAccount CreateDebitAccount()
        => new DebitAccount(new ValueAmount(_balance), _bankInfo, _isVerified);

    private IAccount CreateCreditAccount()
        => new CreditAccount(new CreditValueAmount(_balance), _bankInfo, _isVerified);
}