namespace Banks.Console;

public class Options
{
    public static readonly List<string> CentralBankLogicOptions = new ()
        { CreateBank, CreateAccount, CreateClient, SetBankInfo, CreateTransaction, SkipDays, Exit, };
    public static readonly List<string> MainHandlerOptions = new ()
        { CreateCentralBank, Exit, };
    public static readonly List<string> AccountTypes = new ()
        { CreateDebit, CreateDeposit, CreateCredit, };
    public static readonly List<string> TransactionTypes = new ()
        { OneSideReplenish, OneSideWithdraw, TwoSide, };
    public static int Size => 10;
    public static string Title => "[bold blue]What's your god damn choice[/]?";
    public static string CreateCentralBank => "Create Central Bank";
    public static string CreateBank => "Create bank (string bankName, BankInfo bankInfo)";
    public static string CreateAccount => "Create account (Debit, Deposit, Credit)";
    public static string CreateClient => "Create client";
    public static string CreateDebit => "Debit";
    public static string CreateDeposit => "Deposit";
    public static string CreateCredit => "Credit";
    public static string SkipDays => "Skip n days";
    public static string SetBankInfo => "Set BankInfo for bank (Bank bank, BankInfo)";
    public static string CreateTransaction => "Create transaction";
    public static string OneSideWithdraw => "One-sided withdraw transaction [red](-)[/]";
    public static string OneSideReplenish => "One-sided replenishment transaction [red](+)[/]";
    public static string TwoSide => "Two-sided transaction [red](*)->(.)[/]";
    public static string Exit => "Exit CLI";
    public static string GoBack => "Go back";
}