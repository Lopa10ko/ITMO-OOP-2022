using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService _isuService = new IsuService();

    [Theory]
    [InlineData("M32021", "Lopatenko Gosha")]
    [InlineData("M3421", "Gopatenko Losha")]
    [InlineData("K320212", "Kruglov Gosha")]
    public void AddStudentToGroup_StudentHasGroup(string groupName, string name)
    {
        Group testGroup = _isuService.AddGroup(new GroupName(groupName));
        Student testStudent = _isuService.AddStudent(testGroup, name);
        Assert.Equal(testGroup, testStudent.Group);
    }

    [Theory]
    [InlineData("M32021", "Lopatenko Gosha")]
    [InlineData("M3421", "Gopatenko Losha")]
    [InlineData("K320212", "Kruglov Gosha")]
    public void AddStudentToGroup_GroupContainsStudent(string groupName, string name)
    {
        Group testGroup = _isuService.AddGroup(new GroupName(groupName));
        Student testStudent = _isuService.AddStudent(testGroup, name);
        Assert.Contains(testStudent, testGroup.GetStudents);
    }

    [Theory]
    [InlineData("M33001", "testStudent")]
    public void ReachMaxStudentPerGroup_ThrowException(string groupName, string name)
    {
        const int capacityLimit = 25;
        Group testGroup = _isuService.AddGroup(new GroupName(groupName));
        for (int i = 0; i < capacityLimit; i++)
        {
            _isuService.AddStudent(testGroup, name);
        }

        Assert.Throws<GroupException>(() => _isuService.AddStudent(testGroup, name));
    }

    [Theory]
    [InlineData("M35021")]
    [InlineData("M3002")]
    [InlineData("a3102")]
    [InlineData("M3112345")]
    [InlineData("M310")]
    [InlineData("1310")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName(groupName)));
    }

    [Theory]
    [InlineData("M32021", "K3107", "Lopatenko George")]
    public void TransferStudentToAnotherGroup_GroupChanged(string oldGroupName, string newGroupName, string name)
    {
        Group oldGroup = _isuService.AddGroup(new GroupName(oldGroupName));
        Group newGroup = _isuService.AddGroup(new GroupName(newGroupName));
        Student testStudent = _isuService.AddStudent(oldGroup, name);
        _isuService.ChangeStudentGroup(testStudent, newGroup);
        Assert.Equal(testStudent.Group, newGroup);
        Assert.NotEqual(testStudent.Group, oldGroup);
        Assert.Contains(testStudent, newGroup.GetStudents);
        Assert.DoesNotContain(testStudent, oldGroup.GetStudents);
    }
}