using Isu.Entities;
using Isu.Models;

namespace Isu.Tools;

public class GroupNameException : IsuExceptions
{
    private GroupNameException(string errorMessage)
        : base(errorMessage) { }

    public static GroupNameException GroupNameFormatException(string groupName, string info = "")
        => new GroupNameException($"GroupName {groupName} is invalid: {info}");

    public static GroupNameException InvalidFormatException(char symbol, string info = "")
        => new GroupNameException($"Invalid GroupName symbol {symbol} - {info}");

    public static GroupNameException OutOfRangeException(int value, string type)
        => new GroupNameException($"{type} {value} is invalid: out of range");
}
