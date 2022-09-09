using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Isu.Tools;
using Xunit;
using Xunit.Abstractions;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService _isuService = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group testGroup = _isuService.AddGroup(new GroupName("M32021"));
        Student testStudent = _isuService.AddStudent(testGroup, "Lopatenko Gosha");
        Assert.Contains(testStudent, testGroup.GetStudents());
        Assert.Equal(testGroup, testStudent.Group);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        const int capacityLimit = 25;
        Group testGroup = _isuService.AddGroup(new GroupName("M33001"));
        for (int i = 0; i < capacityLimit; i++)
        {
            _isuService.AddStudent(testGroup, "testStudent");
        }

        Assert.Throws<GroupException>(() => _isuService.AddStudent(testGroup, "testStudent"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("M35021")));
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("M3002")));
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("a3102")));
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("M3112345")));
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("M310")));
        Assert.Throws<GroupNameException>(() => _isuService.AddGroup(new GroupName("M310")));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group oldGroup = _isuService.AddGroup(new GroupName("K3107"));
        Group newGroup = _isuService.AddGroup(new GroupName("M32021"));
        Student testStudent = _isuService.AddStudent(oldGroup, "Lopatenko George");
        _isuService.ChangeStudentGroup(testStudent, newGroup);
        Assert.Equal(testStudent.Group, newGroup);
        Assert.NotEqual(testStudent.Group, oldGroup);
        Assert.Contains(testStudent, newGroup.GetStudents());
        Assert.DoesNotContain(testStudent, oldGroup.GetStudents());
    }
}