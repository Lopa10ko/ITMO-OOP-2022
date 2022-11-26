using Spectre.Console;

namespace Banks.Console.Handlers;

public class CentralBankLogicHandler : Handler
{
    public Handler SuccessorBankCreation => new BankCreationHandler { Successor = this, };
    public Handler SuccessorClientCreation => new ClientCreationHandler { Successor = this, };
    public Handler SuccessorAccountCreation => new AccountCreationHandler { Successor = this, };
    public Handler SuccessorTimeWizard => new TimeWizardHandler { Successor = this, };
    public Handler SuccessorTransaction => new TransactionHandler { Successor = this, };
    public override void HandleRequest(string request, MasterConsole master)
    {
        request = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[green]{request} successfully created![/]\n" + Options.Title)
                .PageSize(Options.Size)
                .AddChoices(GetOptions!));
        AnsiConsole.Clear();
        if (request.Equals(Options.Exit))
            Successor?.HandleRequest(request, master);
        if (request.Equals(Options.CreateBank))
            SuccessorBankCreation.HandleRequest(request, master);
        if (request.Equals(Options.CreateClient))
            SuccessorClientCreation.HandleRequest(request, master);
        if (request.Equals(Options.CreateAccount))
            SuccessorAccountCreation.HandleRequest(request, master);
        if (request.Equals(Options.SkipDays))
            SuccessorTimeWizard.HandleRequest(request, master);
        if (request.Equals(Options.CreateTransaction))
            SuccessorTransaction.HandleRequest(request, master);
    }
}