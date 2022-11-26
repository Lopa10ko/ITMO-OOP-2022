using System.Globalization;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Console.Handlers;

namespace Banks.Console;

public class MasterConsole
{
    private static readonly Lazy<MasterConsole> _lazy = new (() => new MasterConsole());
    private MasterConsole()
    {
        Accounts = new List<Guid>();
        Clients = new List<IClient>();
        Banks = new List<Bank>();
        CentralBankLogicHandler = new CentralBankLogicHandler { GetOptions = Options.CentralBankLogicOptions, };
        MainHandler = new MainHandler { Successor = CentralBankLogicHandler, GetOptions = Options.MainHandlerOptions, };
        CentralBankLogicHandler.Successor = MainHandler;
    }

    public MainHandler MainHandler { get; }
    public CentralBankLogicHandler CentralBankLogicHandler { get; }
    public decimal DaysInYear => new GregorianCalendar().GetDaysInYear(DateTime.Now.Year);
    public CentralBank? CentralBank { get; set; }
    public List<Guid> Accounts { get; }
    public List<IClient> Clients { get; }
    public List<Bank> Banks { get; }
    public static MasterConsole GetInstance() => _lazy.Value;
}