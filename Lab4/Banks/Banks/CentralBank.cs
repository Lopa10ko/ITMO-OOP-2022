using System.Transactions;
using Banks.Accounts;
using Banks.Entities;
using Banks.TimeManager;
using Banks.Tools;

namespace Banks.Banks;

public class CentralBank
{
    private static readonly Lazy<CentralBank> _lazy = new Lazy<CentralBank>(() => new CentralBank(Guid.NewGuid(), TimeMachine.GetInstance()));
    private readonly List<Bank> _banks;

    private CentralBank(Guid id, TimeMachine timeMachine)
    {
        Id = id;
        _banks = new List<Bank>();
        TimeMachine = timeMachine;
    }

    public Guid Id { get; }
    public TimeMachine TimeMachine { get; }

    public IReadOnlyList<Bank> Banks => _banks.AsReadOnly();
    public static CentralBank GetInstance() => _lazy.Value;

    public Bank CreateBank(string bankName, BankInfo bankInfo)
    {
        var bank = new Bank(bankName, bankInfo);
        if (_banks.Contains(bank))
            throw ExistingStateException.ExistingBank(bank);
        TimeMachine.AddObserverBank(bank);
        _banks.Add(bank);
        return bank;
    }

    public void SetBankInfo(Bank bank, BankInfo bankInfo)
    {
        bank.UpdateBankInfo(bankInfo);
    }

    public void CreateTransaction(Guid actorId, Guid recipientId, decimal value)
    {
        Bank actorBank = _banks
            .FirstOrDefault(bank => bank.GetAccounts.Any(account => account.Id.Equals(actorId)))
                         ?? throw AlienEntityException.InvalidBank(actorId);
        Bank recipientBank = _banks
            .FirstOrDefault(bank => bank.GetAccounts.Any(account => account.Id.Equals(recipientId)))
                             ?? throw AlienEntityException.InvalidBank(recipientId);
        if (actorBank.Equals(recipientBank))
            actorBank.CreateTransaction(actorId, recipientId, value);
        else
            actorBank.CreateTransaction(actorId, GetActor(recipientId), value);
    }

    public void CancelTransaction(Guid actorId, Guid recipientId, decimal value)
    {
        Bank actorBank = _banks
            .FirstOrDefault(bank => bank.GetAccounts.Any(account => account.Id.Equals(actorId)))
                         ?? throw AlienEntityException.InvalidBank(actorId);
        Bank recipientBank = _banks
            .FirstOrDefault(bank => bank.GetAccounts.Any(account => account.Id.Equals(recipientId)))
                             ?? throw AlienEntityException.InvalidBank(recipientId);
        if (actorBank.Equals(recipientBank))
            actorBank.CancelTransaction(actorId, recipientId, value);
        else
            actorBank.CancelTransaction(actorId, GetActor(recipientId), value);
    }

    private IAccount GetActor(Guid id)
        => _banks
            .SelectMany(bank => bank.GetAccounts)
            .FirstOrDefault(account => account.Id.Equals(id)) ?? throw AlienEntityException.InvalidAccount(id);
}