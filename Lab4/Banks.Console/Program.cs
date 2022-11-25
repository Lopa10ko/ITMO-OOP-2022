using Spectre.Console;

namespace Banks.Console;

public static class Program
{
    public static void Main()
    {
        var fruit = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What's your [green]god damn choice[/]?")
                .PageSize(5)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(new[]
                {
                    "Apple", "Apricot", "Avocado",
                    "Banana", "Blackcurrant", "Blueberry",
                    "Cherry", "Cloudberry", "Cocunut",
                }));
        AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
    }
}