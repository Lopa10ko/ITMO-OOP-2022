namespace Banks.Tools;

public class ClientBuilderException : BanksException
{
    private ClientBuilderException(string errorMessage)
        : base(errorMessage) { }

    public static ClientBuilderException NoObligatoryProperties(string name, string lastName)
        => new ClientBuilderException($"Invalid Obligatory Properties: ({name} - {lastName}) should not be null or empty");
}