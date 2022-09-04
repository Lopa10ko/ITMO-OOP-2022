using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Models;

public class GroupName
{
    private static string pattern = @"^[A-Z][^089][1-4][\d\D]{2,3}$";
    private string? _groupName;

    public GroupName(string groupName)
    {
        if (!IsEmptyString(groupName))
            Name = groupName;
        CourseNumber = new CourseNumber(GetCourseNumber(groupName));
    }

    public CourseNumber CourseNumber { get; }

    public string Name
    {
        get => this._groupName;
        set
        {
            if (!Regex.IsMatch(value, pattern))
                throw new Exception();
            this._groupName = value;
        }
    }

    public static bool IsEmptyString(string str)
        => string.IsNullOrEmpty(str);

    private static int GetCourseNumber(string groupName)
    {
        int courseNumber;
        try
        {
            courseNumber = int.Parse(groupName.Trim().Substring(2, 3));
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            throw new CourseNumberNotParsableException();
        }

        return courseNumber;
    }
}