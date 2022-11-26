using Spectre.Console;

namespace Banks.Console.Handlers;

public class TimeWizardHandler : Handler
{
    public override void HandleRequest(string request, MasterConsole master)
    {
        int daysToSkip = AnsiConsole.Prompt(
            new TextPrompt<int>($"[italic darkgoldenrod]Provide Days-To-Skip parameter: [/]")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid value[/]")
                .Validate(value =>
                {
                    return value switch
                    {
                        <= 0 => ValidationResult.Error($"[red]Days-To-Skip  parameter must be positive[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        master.CentralBank?.TimeMachine.SkipDays(daysToSkip);
        AnsiConsole.Clear();
        Successor?.HandleRequest($"Skipped {daysToSkip} days, Timer", master);
    }
}