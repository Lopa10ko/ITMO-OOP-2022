﻿using System.Text.RegularExpressions;
using Isu.Extra.Tools;
using Isu.Models;

namespace Isu.Extra.Models;

public record OgnpGroupName : GroupName
{
    private const int OgnpGroupNameLength = 6;
    private const int OgnpGroupLetterPositionStart = 0;
    private const int OgnpGroupLetterPositionLength = 3;
    private const string StudyTypeAndCoursePair = "32";
    private static readonly Regex Validation = new Regex(@"^[А-Я]{3}[0-9]\.[0-9]$", RegexOptions.Compiled);

    public OgnpGroupName(string groupName)
        : base(ReformatGroupName(groupName)) { }

    private static string ReformatGroupName(string groupName)
    {
        ValidateOgnpGroupName(groupName);
        string groupFacultySymbol =
            groupName.Substring(OgnpGroupLetterPositionStart, OgnpGroupLetterPositionLength) switch
            {
                "БИО" => "T",
                "ФИЗ" => "Z",
                "КИБ" => "N",
                "ПРЕ" => "U",
                "ПМИ" => "M",
                "ИКТ" => "K",
                "ФОТ" => "V",
                _ => throw AlienEntityException.AlienFacultyException(groupName.Substring(OgnpGroupLetterPositionStart, OgnpGroupLetterPositionLength)),
            };
        return groupFacultySymbol + StudyTypeAndCoursePair + groupName[3] + groupName[5];
    }

    private static void ValidateOgnpGroupName(string groupName)
    {
        if (groupName.Length != OgnpGroupNameLength)
            throw OgnpGroupNameValueException.InvalidLength(groupName, OgnpGroupNameLength);
        if (!Validation.IsMatch(groupName))
            throw OgnpGroupNameValueException.InvalidFormatting(groupName);
    }
}