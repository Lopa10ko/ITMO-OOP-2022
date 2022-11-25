using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Spectre.Console;

namespace Banks.Console.Handlers;

public class AccountCreationHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        AnsiConsole.Markup("creating [fuchsia]Account[/]...");
        var bankGuids = master.Banks.Select(b => b.Id).ToList();
        var clientGuids = master.Clients.Select(c => c.Id).ToList();
        if (bankGuids.Count != 0 && clientGuids.Count != 0)
        {
            Guid bankId = AnsiConsole.Prompt(
                new SelectionPrompt<Guid>()
                    .Title("[bold fuchsia]Bank Guids[/]\n" + Options.Title)
                    .PageSize(Options.Size)
                    .AddChoices(bankGuids));
            Guid clientId = AnsiConsole.Prompt(
                new SelectionPrompt<Guid>()
                    .Title("[bold fuchsia]Client Guids[/]\n" + Options.Title)
                    .PageSize(Options.Size)
                    .AddChoices(clientGuids));
            IClient client = master.Clients.First(c => c.Id.Equals(clientId));
            Bank bank = master.Banks.First(b => b.Id.Equals(bankId));
            decimal balance = GetValue<decimal>("Initial Account Balance");
            string accountType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold fuchsia]Bank Guids[/]\n" + Options.Title)
                    .PageSize(Options.Size)
                    .AddChoices(Options.AccountTypes));
            Guid account = default;
            if (accountType.Equals(Options.CreateCredit))
            {
                account = bank.CreateAccount<CreditAccount>(client, balance);
            }
            else if (accountType.Equals(Options.CreateDebit))
            {
                account = bank.CreateAccount<DebitAccount>(client, balance);
            }
            else if (accountType.Equals(Options.CreateDeposit))
            {
                account = bank.CreateAccount<DepositAccount>(client, balance);
            }

            master.Accounts.Add(account);
        }

        AnsiConsole.Clear();
        Successor?.HandleRequest("Account", master);
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