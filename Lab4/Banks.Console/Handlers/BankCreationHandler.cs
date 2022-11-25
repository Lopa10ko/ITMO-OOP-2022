using Banks.Banks;
using Banks.Entities;
using Spectre.Console;

namespace Banks.Console.Handlers;

public class BankCreationHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        string bankName = AnsiConsole.Ask<string>("creating [fuchsia]Bank[/]...\n" +
                                                  "[italic darkgoldenrod]Provide bankName: [/]");
        int commissionDay = GetValue<int>("commissionDay");
        long freezeTime = GetValue<long>("freezeTime");
        decimal debitPercent = GetValue<decimal>("debitPercent") / master.DaysInYear;
        decimal untrustedUserWithdrawLimit = GetValue<decimal>("untrustedUserWithdrawLimit");
        decimal creditLimit = GetValue<decimal>("creditLimit");
        decimal creditCommission = GetValue<decimal>("creditCommission");
        int count = GetValue<int>("DepositRules list length");
        DepositRules.DepositRulesBuilder builderDepositRules = DepositRules.Builder;
        for (int i = 0; i < count; i++)
        {
            decimal criticalValue = GetValue<decimal>($"DepositRule CriticalValue #{i + 1}");
            decimal percent = GetValue<decimal>($"DepositRule Percent #{i + 1}") / master.DaysInYear;
            builderDepositRules.AddDepositRule(new DepositRule(criticalValue, percent));
        }

        DepositRules depositRules = builderDepositRules.Build();
        var bankInfo = new BankInfo(
            commissionDay,
            freezeTime,
            debitPercent,
            untrustedUserWithdrawLimit,
            creditLimit,
            creditCommission,
            depositRules);
        var bank = new Bank(bankName, bankInfo);
        master.Banks.Add(bank);
        AnsiConsole.Clear();
        Successor?.HandleRequest($"Bank {bank.Id}", master);
    }

    private static T GetValue<T>(string parameterType)
    {
        T parameter = AnsiConsole.Prompt(
            new TextPrompt<T>($"[italic darkgoldenrod]Provide {parameterType} ({typeof(T)}): [/]")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid value[/]")
                .Validate(value =>
                {
                    return value switch
                    {
                        <= 0 => ValidationResult.Error($"[red]{parameterType}  parameter must be positive[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        return parameter;
    }
}