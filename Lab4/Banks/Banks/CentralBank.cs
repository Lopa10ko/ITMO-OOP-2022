using Banks.Models;
using Banks.TimeManager;

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
            throw new Exception("bank has been created already");
        TimeMachine.Banks.Add(bank);
        _banks.Add(bank);
        return bank;
    }

    public void SetBankInfo(Bank bank, BankInfo bankInfo)
    {
        bank.BankInfo = bankInfo;
    }
}