using Banks.Accounts;
using Banks.Banks;
using Banks.Tools;
using Spectre.Console;

namespace Banks.Console.Handlers;

public class TransactionHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[bold yellow]Choose transaction type:[/]\n")
                .PageSize(Options.Size)
                .AddChoices(Options.TransactionTypes));
        if (choice.Equals(Options.TwoSide) && master.Accounts.Count >= 2)
        {
            Guid firstId = AnsiConsole.Prompt(
                new SelectionPrompt<Guid>()
                    .Title($"[bold yellow]Choose transaction type:[/]\n")
                    .PageSize(Options.Size)
                    .AddChoices(master.Accounts));
            Guid secondId = AnsiConsole.Prompt(
                new SelectionPrompt<Guid>()
                    .Title($"[bold yellow]Choose transaction type:[/]\n")
                    .PageSize(Options.Size)
                    .AddChoices(master.Accounts.Where(g => !g.Equals(firstId))));
            int balance = AnsiConsole.Prompt(
                new TextPrompt<int>($"[italic darkgoldenrod]Provide MoneyAmount parameter: [/]")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid value[/]")
                    .Validate(value =>
                    {
                        return value switch
                        {
                            <= 0 => ValidationResult.Error($"[red]MoneyAmount parameter must be positive[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
            try
            {
                master.CentralBank?.CreateTransaction(firstId, secondId, balance);
            }
            catch (ValueAmountException e)
            {
                AnsiConsole.Markup(e.Message);
            }
        }
        else if (choice.Equals(Options.OneSideReplenish) || choice.Equals(Options.OneSideWithdraw))
        {
            var bankGuids = master.Banks.Select(b => b.Id).ToList();
            Guid bankId = default;
            if (bankGuids.Count != 0)
            {
                bankId = AnsiConsole.Prompt(
                    new SelectionPrompt<Guid>()
                        .Title("[bold fuchsia]Bank Guids to search for account[/]\n" + Options.Title)
                        .PageSize(Options.Size)
                        .AddChoices(bankGuids));
            }

            Bank bank = master.Banks.First(b => b.Id.Equals(bankId));
            Guid accountId = AnsiConsole.Prompt(
                new SelectionPrompt<Guid>()
                    .Title($"[bold yellow]Choose transaction type:[/]\n")
                    .PageSize(Options.Size)
                    .AddChoices(bank.GetAccounts.Select(a => a.Id)));
            IAccount account = bank.GetAccounts.First(a => a.Id.Equals(accountId));
            decimal balance = GetValue<decimal>("Initial Account Balance");
            try
            {
                if (choice.Equals(Options.OneSideReplenish))
                    bank.CreateReplenishmentTransaction(accountId, balance);
                else
                    bank.CreateWithdrawTransaction(accountId, balance);
            }
            catch (Exception e)
            {
                AnsiConsole.Markup(e.Message);
            }

            string? accountMoney = bank.GetAccounts.First(a => a.Id.Equals(accountId)).ToString();
            request += accountMoney + ":";
        }

        AnsiConsole.Clear();
        Successor?.HandleRequest($"Transaction handled,", master);
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
                        <= 0m => ValidationResult.Error($"[red]{parameterType}  parameter must be positive[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        return parameter;
    }
}