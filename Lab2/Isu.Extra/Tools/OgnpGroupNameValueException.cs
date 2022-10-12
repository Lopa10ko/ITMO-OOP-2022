namespace Isu.Extra.Tools;

public class OgnpGroupNameValueException : IsuExtraException
{
    private OgnpGroupNameValueException(string errorMessage)
        : base(errorMessage) { }

    public static OgnpGroupNameValueException InvalidLength(string groupName, int ognpGroupNameLength)
        => new OgnpGroupNameValueException($"Length of OgnpGroupName {groupName} should be {ognpGroupNameLength}");

    public static OgnpGroupNameValueException InvalidFormatting(string groupName)
        => new OgnpGroupNameValueException($"OgnpGroupName {groupName} is in invalid format");
}