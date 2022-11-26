using Banks.Clients;
using Spectre.Console;

namespace Banks.Console.Handlers;

public class ClientCreationHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        string name = AnsiConsole.Ask<string>("[italic darkgoldenrod]Provide client name: [/]");
        string lastName = AnsiConsole.Ask<string>("[italic darkgoldenrod]Provide client lastname: [/]");
        string address = AnsiConsole.Ask<string>("[italic darkgoldenrod]Provide client address: [/]");
        string passport = AnsiConsole.Ask<string>("[italic darkgoldenrod]Provide client passport: [/]");
        master.Clients.Add(Client.Builder.WithName(name).WithLastName(lastName).WithAddress(address).WithPassport(passport).Build());
        AnsiConsole.Clear();
        Successor?.HandleRequest(request, master);
    }
}