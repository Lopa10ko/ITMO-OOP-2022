using Banks.Banks;
using Spectre.Console;

namespace Banks.Console.Handlers;

public abstract class Handler
{
    public Handler? Successor { get; set; }
    public List<string>? GetOptions { get; set; }
    public abstract void HandleRequest(string request, MasterConsole master);
}

public class MainHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        if (request.Equals(Options.CreateCentralBank))
        {
            master.CentralBank = CentralBank.GetInstance();
            Successor?.HandleRequest("Central Bank", master);
        }

        if (request.Equals(Options.Exit))
        {
            AnsiConsole.Markup("\n[blue1]OKAY, GOODBYE[/]\n");
        }
    }
}