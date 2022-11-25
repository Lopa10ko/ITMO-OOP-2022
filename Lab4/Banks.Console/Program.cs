using Banks.Banks;
using Spectre.Console;

namespace Banks.Console;

public static class Program
{
    public static void Main()
    {
        var master = MasterConsole.GetInstance();
        string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(Options.Title)
                .PageSize(Options.Size)
                .AddChoices(new[] { Options.CreateCentralBank, Options.Exit, }));
        AnsiConsole.Clear();
        master.MainHandler.HandleRequest(choice, master);
    }
}